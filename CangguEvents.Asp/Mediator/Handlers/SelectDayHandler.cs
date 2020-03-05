using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Asp.Mediator.Base;
using CangguEvents.Asp.Models;
using CangguEvents.Asp.Models.Commands;
using CangguEvents.Asp.Models.Responses;
using CangguEvents.Asp.Services;
using CangguEvents.Asp.Services.Base;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Asp.Mediator.Handlers
{
    public class SelectDayHandler : IRequestHandlerDomain<SelectDayCommand>
    {
        public async Task<IReadOnlyCollection<ITelegramResponse>> Handle(SelectDayCommand request,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var buttons = ((DayOfWeek[]) Enum.GetValues(typeof(DayOfWeek)))
                .Select(SelectInlineButton)
                .ToList();

            return new[] {new KeyboardTelegramResponse(new InlineKeyboardMarkup(buttons), "Select day")};
        }

        private static InlineKeyboardButton SelectInlineButton(DayOfWeek dayOfWeek)
        {
            return InlineKeyboardButton.WithCallbackData(ShortDayNames.Get(dayOfWeek),
                $"{CommandMessages.CallbackDay}:{(int) dayOfWeek}");
        }
    }
}