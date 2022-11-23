using MediatR;
using VerticalSliceArchitecture.Core.Common.Errors;
using VerticalSliceArchitecture.Core.Common.Interfaces;

namespace VerticalSliceArchitecture.Common.Extensions;
public static class RoutesExtensions
{
    public static RouteHandlerBuilder MediatrGet<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        return group.MapGet(template, async (IMediator mediator, [AsParameters] TRequest request) =>
        {
            return await HandleResult<TRequest, TResponse>(mediator, request);
        });
    }



    public static RouteHandlerBuilder MediatrPost<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapPost(template, async (IMediator mediator, [AsParameters] TRequest request) =>
        {
            return await HandleVoidResult(mediator, request);
        });
    }


    public static RouteHandlerBuilder MediatrPost<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        return group.MapPost(template, async (IMediator mediator, [AsParameters] TRequest request) =>
        {
            return await HandleResult<TRequest, TResponse>(mediator, request);
        });
    }



    public static RouteHandlerBuilder MediatrPut<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        return group.MapPut(template, async (IMediator mediator, [AsParameters] TRequest request) =>
        {
            return await HandleResult<TRequest, TResponse>(mediator, request);
        });
    }

    public static RouteHandlerBuilder MediatrPut<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapPut(template, async (IMediator mediator, [AsParameters] TRequest request) =>
        {
            return await HandleVoidResult(mediator, request);
        });
    }

    public static RouteHandlerBuilder MediatrDelete<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapDelete(template, async (IMediator mediator, [AsParameters] TRequest request) =>
        {
            return await HandleVoidResult(mediator, request);
        });
    }


    private static async Task<IResult> HandleResult<TRequest, TResponse>(IMediator mediator, TRequest request)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        FluentResults.Result<TResponse> results = await mediator.Send(request);

        if (results.IsSuccess)
        {
            return TypedResults.Ok(results.Value);
        }

        FluentResults.IError? error = results.Errors.FirstOrDefault();

        return error switch
        {
            ValidationError validationError => Results.ValidationProblem(
                validationError.Failures
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            ),
            NotFoundError notFound => Results.NotFound(notFound.Message),
            _ => Results.Problem(detail: error?.Message ?? "An error has ocurred.")
        };
    }

    private static async Task<IResult> HandleVoidResult<TRequest>(IMediator mediator, TRequest request) where TRequest : IHttpRequest
    {
        FluentResults.Result results = await mediator.Send(request);

        if (results.IsSuccess)
        {
            return TypedResults.Ok();
        }

        FluentResults.IError? error = results.Errors.FirstOrDefault();

        return error switch
        {
            ValidationError validationError => Results.ValidationProblem(
                validationError.Failures
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            ),
            NotFoundError notFound => Results.NotFound(notFound.Message),
            _ => Results.Problem(detail: error?.Message ?? "An error has ocurred.")
        };
    }

}
