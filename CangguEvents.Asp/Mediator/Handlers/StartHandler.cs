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
    public class StartHandler : IRequestHandlerDomain<StartCommand>, IRequestHandlerDomain<BackCommand>
    {
        private readonly IUserStateRepository _stateRepository;

        public StartHandler(IUserStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<IReadOnlyCollection<ITelegramResponse>> Handle(StartCommand request,
            CancellationToken cancellationToken)
        {
            var userState = new UserState(true, true);
            
            await _stateRepository.CreateUser(request.UserId, userState, cancellationToken);
            return StartKeyboard();
        }

        public Task<IReadOnlyCollection<ITelegramResponse>> Handle(BackCommand request,
            CancellationToken cancellationToken)
        {
            var startKeyboard = StartKeyboard();
            return Task.FromResult(startKeyboard);
        }

        private static IReadOnlyCollection<ITelegramResponse> StartKeyboard()
        {
            ReplyKeyboardMarkup replyKeyboard = new[]
            {
                new[] {CommandMessages.Today, CommandMessages.WholeWeek, CommandMessages.OneDay},
                new[] {CommandMessages.Settings},
            };
            replyKeyboard.ResizeKeyboard = true;

            return new[] {new KeyboardTelegramResponse(replyKeyboard, "Choose")};
        }
    }
}