namespace CangguEvents.Application.Models.Commands
{
    public class SettingsCommand : MessageCommandBase
    {
        public SettingsCommand(in long userId) : base(userId)
        {
        }
    }
}