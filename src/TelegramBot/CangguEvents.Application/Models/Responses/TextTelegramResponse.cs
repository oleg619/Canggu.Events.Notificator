using CangguEvents.Application.Services.Base;

namespace CangguEvents.Application.Models.Responses
{
    public class TextTelegramResponse : ITelegramResponse
    {
        public readonly string Text;

        public TextTelegramResponse(string text)
        {
            Text = text;
        }
    }
}