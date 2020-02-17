using CangguEvents.Asp.Services.Base;
using CangguEvents.Asp.Services.Implementation;

namespace CangguEvents.Asp.Models.Responses
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