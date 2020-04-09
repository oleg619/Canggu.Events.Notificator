namespace CangguEvents.Application.Models.Commands
{
    public class UnknownCommand : MessageCommandBase
    {
        public UnknownCommand(long userId) : base(userId)
        {
        }
    }
}