using CangguEvents.Application.Services.Base;

namespace CangguEvents.Application.Models.Responses
{
    public class PhotoTelegramResponse : ITelegramResponse
    {
        public readonly byte[] Image;

        public readonly string Caption;

        public PhotoTelegramResponse(byte[] image, string caption)
        {
            Image = image;
            Caption = caption;
        }
    }
}