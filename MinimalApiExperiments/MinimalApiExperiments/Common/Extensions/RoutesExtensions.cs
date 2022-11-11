using MediatR;
using MinimalApiExperiments.ApplicationCore.Common.Interfaces;

namespace MinimalApiExperiments.Common.Extensions;
public static class RoutesExtensions
{

    public static RouteHandlerBuilder MediatrGet<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapGet(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        )
        .WithName(typeof(TRequest).Name);
    }

    public static RouteHandlerBuilder MediatrPost<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapPost(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        )
        .WithName(typeof(TRequest).Name);
    }

    public static RouteHandlerBuilder MediatrPut<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapPut(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        )
        .WithName(typeof(TRequest).Name);
    }

    public static RouteHandlerBuilder MediatrDelete<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapDelete(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        )
        .WithName(typeof(TRequest).Name);
    }
}
