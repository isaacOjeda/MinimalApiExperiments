using MediatR;
using VerticalSliceArchitecture.ApplicationCore.Common.Interfaces;

namespace VerticalSliceArchitecture.Common.Extensions;
public static class RoutesExtensions
{

    public static RouteHandlerBuilder MediatrGet<TRequest>(this RouteGroupBuilder group, string template) where TRequest : IHttpRequest =>
        group.MapGet(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request))
        .WithName(typeof(TRequest).Name);

    public static RouteHandlerBuilder MediatrPost<TRequest>(this RouteGroupBuilder group, string template) where TRequest : IHttpRequest =>
        group.MapPost(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request))
        .WithName(typeof(TRequest).Name);


    public static RouteHandlerBuilder MediatrPut<TRequest>(this RouteGroupBuilder group, string template) where TRequest : IHttpRequest =>
        group.MapPut(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request))
        .WithName(typeof(TRequest).Name);


    public static RouteHandlerBuilder MediatrDelete<TRequest>(this RouteGroupBuilder group, string template) where TRequest : IHttpRequest =>
        group.MapDelete(template, (IMediator mediator, [AsParameters] TRequest request) =>
            mediator.Send(request))
        .WithName(typeof(TRequest).Name);

}
