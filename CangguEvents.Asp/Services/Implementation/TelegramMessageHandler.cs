using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CangguEvents.Asp.Models;
using CangguEvents.Asp.Models.Responses;
using CangguEvents.Asp.Services.Base;
using MediatR;
using Serilog;
using Telegram.Bot.Types;

namespace CangguEvents.Asp.Services.Implementation
{
    public class TelegramMessageHandler
    {
        private readonly MessageParser _messageParser;
        private readonly IMediator _mediator;
        private readonly IMessengerSender _sender;
        private readonly ILogger _logger;

        public TelegramMessageHandler(
            MessageParser messageParser,
            IMediator mediator,
            IMessengerSender sender,
            ILogger logger)
        {
            _messageParser = messageParser;
            _sender = sender;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(Update update)
        {
            var (command, answer) = _messageParser.ParseMessage(update);

            _logger.Information("Parse {message} to {command}", update, command.GetType());
            var telegramResponses = (await _mediator.Send(command)).ToList();

            if (answer.CallbackQueryId != null && telegramResponses.Count > 0)
            {
                telegramResponses.Add(new AnswerToCallback(answer.CallbackQueryId));
            }

            await SendMessages(telegramResponses, answer);
        }

        private async Task SendMessages(IEnumerable<ITelegramResponse> telegramResponses, ResponseInfo responseInfo)
        {
            var (userId, messageId) = responseInfo;
            foreach (var telegramResponse in telegramResponses)
            {
                _logger.Information("Send {message}", telegramResponse);
                await SendMessage(telegramResponse, userId, messageId);
            }
        }

        private Task SendMessage(ITelegramResponse telegramResponse, long chatId, int messageId)
        {
            return telegramResponse switch
            {
                TextTelegramResponse text => _sender.SendText(text, chatId),
                PhotoTelegramResponse photo => _sender.SendPhoto(photo, chatId),
                LocationTelegramResponse location => _sender.SendLocation(location, chatId),
                KeyboardTelegramResponse keyboard => _sender.SendKeyboard(keyboard, chatId),
                EditKeyboardTelegramResponse response => _sender.EditMessageText(response, messageId, chatId),
                AnswerToCallback callback => _sender.AnswerToCallback(callback.CallbackId),

                _ => throw new ArgumentOutOfRangeException(nameof(telegramResponse),
                    telegramResponse.GetType().FullName, "Unknown command")
            };
        }
    }
}