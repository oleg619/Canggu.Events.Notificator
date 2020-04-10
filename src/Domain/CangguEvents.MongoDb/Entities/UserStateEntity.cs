using CangguEvents.Domain;
using MongoDB.Bson.Serialization.Attributes;

namespace CangguEvents.MongoDb.Entities
{
    public class UserStateEntity
    {
        [BsonId]
        public long UserId { get; set; }

        public bool Subscribed { get; set; }
        public bool ShortInfo { get; set; }

        public static UserStateEntity From(UserState state, long userId)
        {
            return new UserStateEntity
            {
                UserId = userId,
                Subscribed = state.Subscribed,
                ShortInfo = state.ShortInfo
            };
        }

        public UserState ToDomain()
        {
            return new UserState(Subscribed, ShortInfo);
        }
    }
}