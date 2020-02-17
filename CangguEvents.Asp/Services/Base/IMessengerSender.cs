using System.Threading.Tasks;
using CangguEvents.Asp.Models.Responses;
using Shared;

namespace CangguEvents.Asp.Services.Base
{
    public interface IMessengerSender
    {
        Task SendKeyboard(KeyboardTelegramResponse response, in long chatId);
        Task SendLocation(LocationTelegramResponse response, in long chatId);
        Task SendText(TextTelegramResponse response, in long chatId);
        Task SendPhoto(PhotoTelegramResponse response, long chatId);
        Task EditMessageText(EditKeyboardTelegramResponse response, int messageId, in long chatId);
        Task AnswerToCallback(string callbackQueryId);
    }
}