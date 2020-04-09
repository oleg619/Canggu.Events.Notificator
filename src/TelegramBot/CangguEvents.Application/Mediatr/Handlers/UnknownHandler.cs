using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CangguEvents.Application.Mediatr.Base;
using CangguEvents.Application.Models.Commands;
using CangguEvents.Application.Models.Responses;
using CangguEvents.Application.Services.Base;

namespace CangguEvents.Application.Mediatr.Handlers
{
    public class UnknownHandler : IRequestHandlerDomain<UnknownCommand>
    {
        public async Task<IReadOnlyCollection<ITelegramResponse>> Handle(UnknownCommand request,
            CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return new[] {new TextTelegramResponse("Unknown error")};
        }
    }
}