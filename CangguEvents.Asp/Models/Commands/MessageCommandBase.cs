using System.Collections.Generic;
using CangguEvents.Asp.Services.Base;
using MediatR;

namespace CangguEvents.Asp.Models.Commands
{
    public abstract class MessageCommandBase : IRequest<IEnumerable<ITelegramResponse>>
    {
        public readonly long UserId;

        protected MessageCommandBase(long userId)
        {
            UserId = userId;
        }
    }
}