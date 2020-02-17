using CangguEvents.Asp.Services.Base;
using CangguEvents.Asp.Services.Implementation;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Asp.Models.Responses
{
    public class KeyboardTelegramResponse : ITelegramResponse
    {
        public readonly IReplyMarkup KeyboardMarkup;
        public readonly string Text;

        public KeyboardTelegramResponse(IReplyMarkup keyboardMarkup, string text)
        {
            KeyboardMarkup = keyboardMarkup;
            Text = text;
        }
    }

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