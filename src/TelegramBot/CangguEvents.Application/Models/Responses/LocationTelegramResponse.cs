using CangguEvents.Application.Services.Base;
using CangguEvents.Domain;

namespace CangguEvents.Application.Models.Responses
{
    public class LocationTelegramResponse : ITelegramResponse
    {
        public readonly Location Location;

        public LocationTelegramResponse(Location location)
        {
            Location = location;
        }
    }

    public class AnswerToCallback : ITelegramResponse
    {
        public string CallbackId { get; }

        public AnswerToCallback(string callbackId)
        {
            CallbackId = callbackId;
        }
    }
}