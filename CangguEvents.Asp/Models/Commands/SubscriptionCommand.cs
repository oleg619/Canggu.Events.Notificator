using Shared;

namespace CangguEvents.Asp.Models.Commands
{
    public class SubscriptionCommand : MessageCommandBase
    {
        public bool Subscribe { get; }

        public SubscriptionCommand(bool subscribe, in long userId) : base(userId)
        {
            Subscribe = subscribe;
        }
    }
}