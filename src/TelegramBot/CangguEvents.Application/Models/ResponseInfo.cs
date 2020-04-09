namespace CangguEvents.Application.Models
{
    public readonly struct ResponseInfo
    {
        public readonly long UserId;
        public readonly int MessageId;
        public readonly string? CallbackQueryId;

        public ResponseInfo(long userId, int messageId, string? callbackQueryId = null)
        {
            UserId = userId;
            MessageId = messageId;
            CallbackQueryId = callbackQueryId;
        }

        public void Deconstruct(out long userId, out int messageId, out string? callbackQueryId)
        {
            userId = UserId;
            messageId = MessageId;
            callbackQueryId = CallbackQueryId;
        }
        
        public void Deconstruct(out long userId, out int messageId)
        {
            userId = UserId;
            messageId = MessageId;
        }
    }
}