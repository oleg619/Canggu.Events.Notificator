using CangguEvents.Application.Services.Base;
using Telegram.Bot.Types.ReplyMarkups;

namespace CangguEvents.Application.Models.Responses
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
}