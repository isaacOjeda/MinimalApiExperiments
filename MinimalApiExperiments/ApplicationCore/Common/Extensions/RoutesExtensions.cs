using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ApplicationCore.Common.Extensions;
public static class RoutesExtensions
{

    public static RouteGroupBuilder MediatrGet<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        group.MapGet(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        );


        return group;
    }

    public static RouteGroupBuilder MediatrPost<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        group.MapPost(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        );


        return group;
    }
}
