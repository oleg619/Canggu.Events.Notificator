using CangguEvents.Application.Services.Base;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Application.Models.Responses
{
    public class EditKeyboardTelegramResponse : ITelegramResponse
    {
        public readonly InlineKeyboardMarkup Keyboard;
        public readonly string Text;

        public EditKeyboardTelegramResponse(InlineKeyboardMarkup keyboard, string text)
        {
            Keyboard = keyboard;
            Text = text;
        }
    }
}