using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Application.Mediatr.Base;
using CangguEvents.Application.Models;
using CangguEvents.Application.Models.Commands;
using CangguEvents.Application.Models.Responses;
using CangguEvents.Application.Services;
using CangguEvents.Application.Services.Base;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Application.Mediatr.Handlers
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