using CangguEvents.Asp.Services.Base;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Asp.Models.Responses
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