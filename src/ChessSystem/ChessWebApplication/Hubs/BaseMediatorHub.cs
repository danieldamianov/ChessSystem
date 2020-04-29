using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace ChessWebApplication.Hubs
{
    public class BaseMediatorHub : Hub
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private IMediator mediator;

        public BaseMediatorHub(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected IMediator Mediator
            => this.mediator ??= (IMediator)this.httpContextAccessor.HttpContext
                .RequestServices
                .GetService(typeof(IMediator));
    }
}
