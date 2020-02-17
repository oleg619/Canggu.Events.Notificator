using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CangguEvents.Asp.Models;
using CangguEvents.Asp.Models.Commands;
using Serilog;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CangguEvents.Asp.Services.Implementation
{
    public class MessageParser
    {
        private readonly ILogger _logger;

        public MessageParser(ILogger logger)
        {
            _logger = logger;
        }

        public (MessageCommandBase messageCommandBase, ResponseInfo answerInfo) ParseMessage(Update update)
        {
            long chatId;
            string message;

            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    chatId = update.Message.Chat.Id;
                    message = update.Message.Text;

                    var textTypeToCommandBase = TextTypeToCommandBase(message, chatId);
                    return (textTypeToCommandBase, new ResponseInfo(chatId, update.Message.MessageId));
                }
                case UpdateType.CallbackQuery:
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    message = update.CallbackQuery.Data;
                    var callbackCommand = GetCallbackCommand(message, chatId);

                    var responseInfo = new ResponseInfo(chatId, update.CallbackQuery.Message.MessageId,
                        update.CallbackQuery.Id);
                    
                    return (callbackCommand, responseInfo);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static MessageCommandBase GetCallbackCommand(string message, long chatId)
        {
            var strings = message.Split(":");
            var command = strings[0];
            var commandId = strings[1];

            return command switch
            {
                "full" => new FullEventInfoCommand(int.Parse(commandId), chatId),
                CommandMessages.CallbackHide => new ShortEventInfoCommand(int.Parse(commandId), chatId),
                CommandMessages.CallbackDay => EventsCommand.Day((DayOfWeek) int.Parse(commandId), chatId),
                _ => throw new NotImplementedException()
            };
        }

        private MessageCommandBase TextTypeToCommandBase(string message, long chatId)
        {
            message = Regex.Replace(message, @"[^\u0000-\u007F]+", string.Empty);

            var func = CommandsToMessage
                .Where(pair => pair.Key.Contains(message, StringComparison.OrdinalIgnoreCase))
                .Select(pair => pair.Value)
                .FirstOrDefault();

            if (func != default)
            {
                return func(chatId);
            }

            _logger.Warning("Cannot parse {message} from {userId}", message, chatId);
            return new UnknownCommand(chatId);
        }

        private static readonly Dictionary<string, Func<long, MessageCommandBase>> CommandsToMessage =
            new Dictionary<string, Func<long, MessageCommandBase>>
            {
                {CommandMessages.Start, id => new StartCommand(id)},
                {CommandMessages.OneDay, id => new SelectDayCommand(id)},
                {CommandMessages.WholeWeek, id => EventsCommand.WholeWeek(id)},
                {CommandMessages.Today, id => EventsCommand.Day(DateTimeService.CangguTimeNow.DayOfWeek, id)},
                {CommandMessages.Settings, id => new SettingsCommand(id)},
                {CommandMessages.Back, id => new BackCommand(id)},
                {CommandMessages.Subscribe, id => new SubscriptionCommand(true, id)},
                {CommandMessages.Unsubscribe, id => new SubscriptionCommand(false, id)},
                {CommandMessages.ShortInfo, id => new LengthInfoCommand(true, id)},
                {CommandMessages.FullInfo, id => new LengthInfoCommand(false, id)}
            };
    }
}