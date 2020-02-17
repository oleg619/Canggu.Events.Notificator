namespace CangguEvents.Asp.Models.Commands
{
    public class UnknownCommand : MessageCommandBase
    {
        public UnknownCommand(long userId) : base(userId)
        {
        }
    }
}