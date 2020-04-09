using System.Collections.Generic;
using CangguEvents.Application.Models.Commands;
using CangguEvents.Application.Services.Base;
using MediatR;

namespace CangguEvents.Application.Mediatr.Base
{
    public interface IRequestHandlerDomain<in TRequest> : IRequestHandler<TRequest, IReadOnlyCollection<ITelegramResponse>>
        where TRequest : MessageCommandBase
    {
    }
}