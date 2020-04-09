using System.ComponentModel.DataAnnotations;
using CangguEvents.Domain;

namespace CangguEvents.SQLite.Entities
{
    public class UserStateEntity
    {
        [Key]
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