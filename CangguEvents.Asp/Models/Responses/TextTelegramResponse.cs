using CangguEvents.Asp.Services.Base;

namespace CangguEvents.Asp.Models.Responses
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