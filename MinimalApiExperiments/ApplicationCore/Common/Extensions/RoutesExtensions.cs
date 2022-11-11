using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiExperiments.ApplicationCore.Common;

namespace MinimalApiExperiments.ApplicationCore.Common.Extensions;
public static class RoutesExtensions
{

    public static RouteGroupBuilder MediatrGet<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        group.MapGet(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        );


        return group;
    }

    public static RouteGroupBuilder MediatrPost<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        group.MapPost(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        );


        return group;
    }

    public static RouteGroupBuilder MediatrPut<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        group.MapPut(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        );


        return group;
    }

    public static RouteGroupBuilder MediatrDelete<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        group.MapDelete(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request)
        );


        return group;
    }
}
