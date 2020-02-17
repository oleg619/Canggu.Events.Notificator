using CangguEvents.Asp.Services.Base;
using CangguEvents.Asp.Services.Implementation;
using Shared;

namespace CangguEvents.Asp.Models.Responses
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