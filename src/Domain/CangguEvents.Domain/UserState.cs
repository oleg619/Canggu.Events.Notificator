using System;
using System.Diagnostics.Contracts;

namespace CangguEvents.Domain
{
    public struct UserState
    {
        public bool Subscribed { get; }
        public bool ShortInfo { get; }

        public UserState(bool subscribed, bool shortInfo)
        {
            Subscribed = subscribed;
            ShortInfo = shortInfo;
        }

        [Pure]
        public UserState ChangeSubscribe() => new UserState(!Subscribed, ShortInfo);

        [Pure]
        public UserState ChangeShortInfo() => new UserState(Subscribed, !ShortInfo);

        public static bool operator ==(UserState c1, UserState c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(UserState c1, UserState c2)
        {
            return !c1.Equals(c2);
        }


        public bool Equals(UserState other)
        {
            return Subscribed == other.Subscribed && ShortInfo == other.ShortInfo;
        }

        public override bool Equals(object obj)
        {
            return obj is UserState other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subscribed, ShortInfo);
        }
    }
}