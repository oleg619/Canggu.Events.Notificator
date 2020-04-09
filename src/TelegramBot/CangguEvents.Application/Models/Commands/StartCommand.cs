namespace CangguEvents.Application.Models.Commands
{
    public class StartCommand : MessageCommandBase
    {
        public StartCommand(in long userId) : base(userId)
        {
        }
    }
}