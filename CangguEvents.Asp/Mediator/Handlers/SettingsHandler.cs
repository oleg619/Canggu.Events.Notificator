using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Asp.Mediator.Base;
using CangguEvents.Asp.Models;
using CangguEvents.Asp.Models.Commands;
using CangguEvents.Asp.Models.Responses;
using CangguEvents.Asp.Services.Base;
using Shared;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Asp.Mediator.Handlers
{
    public class SettingsHandler : IRequestHandlerDomain<SettingsCommand>, IRequestHandlerDomain<SubscriptionCommand>,
        IRequestHandlerDomain<LengthInfoCommand>
    {
        private readonly IUserStateRepository _userStateRepository;

        public SettingsHandler(IUserStateRepository userStateRepository)
        {
            _userStateRepository = userStateRepository;
        }

        public async Task<IEnumerable<ITelegramResponse>> Handle(SettingsCommand settingsCommand,
            CancellationToken cancellationToken)
        {
            var userState = await _userStateRepository.GetUserState(settingsCommand.UserId);

            var replyKeyboard = GetSettingReplyKeyboardMarkupFor(userState);

            var response = new[] {new KeyboardTelegramResponse(replyKeyboard, "Choose")};
            return response;
        }

        public async Task<IEnumerable<ITelegramResponse>> Handle(SubscriptionCommand request,
            CancellationToken cancellationToken)
        {
            var userState = await _userStateRepository.GetUserState(request.UserId);

            if (userState.Subscribed != request.Subscribe)
            {
                userState = userState.ChangeSubscribe();
                await _userStateRepository.ChangeUserState(request.UserId, userState);
            }

            var replyKeyboard = GetSettingReplyKeyboardMarkupFor(userState);
            var text = $"You successfully {(userState.Subscribed ? "subscribed" : "unsubscribed")}";
            return new[] {new KeyboardTelegramResponse(replyKeyboard, text)};
        }

        public async Task<IEnumerable<ITelegramResponse>> Handle(LengthInfoCommand request,
            CancellationToken cancellationToken)
        {
            var userState = await _userStateRepository.GetUserState(request.UserId);

            if (userState.ShortInfo != request.ShortInfo)
            {
                userState = userState.ChangeShortInfo();
                await _userStateRepository.ChangeUserState(request.UserId, userState);
            }

            var replyKeyboard = GetSettingReplyKeyboardMarkupFor(userState);
            var text = $"You successfully changed info to {(userState.ShortInfo ? "short" : "long")}";
            return new[] {new KeyboardTelegramResponse(replyKeyboard, text)};
        }

        private static ReplyKeyboardMarkup GetSettingReplyKeyboardMarkupFor(UserState userState)
        {
            ReplyKeyboardMarkup replyKeyboard = new[]
            {
                new[]
                {
                    //26D4
                    userState.Subscribed ? CommandMessages.Unsubscribe : CommandMessages.Subscribe,
                    userState.ShortInfo ? CommandMessages.FullInfo : CommandMessages.ShortInfo
                },
                new[] {CommandMessages.Back},
            };
            replyKeyboard.ResizeKeyboard = true;
            return replyKeyboard;
        }
    }
}