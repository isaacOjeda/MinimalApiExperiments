using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using VerticalSliceArchitecture.Core.Common.Errors;
using VerticalSliceArchitecture.Core.Common.Interfaces;

namespace VerticalSliceArchitecture.Common.Extensions;

public static class RoutesExtensions
{
    public static RouteHandlerBuilder MediatrGet<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        return group.MapGet(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await HandleResult<TRequest, TResponse>(mediator, request));
    }


    public static RouteHandlerBuilder MediatrPost<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapPost(template,
            async (IMediator mediator, [AsParameters] TRequest request) => await HandleVoidResult(mediator, request));
    }


    public static RouteHandlerBuilder MediatrPost<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        return group.MapPost(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await HandleResult<TRequest, TResponse>(mediator, request));
    }


    public static RouteHandlerBuilder MediatrPut<TRequest, TResponse>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        return group.MapPut(template,
            async (IMediator mediator, [AsParameters] TRequest request) =>
                await HandleResult<TRequest, TResponse>(mediator, request));
    }

    public static RouteHandlerBuilder MediatrPut<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapPut(template,
            async (IMediator mediator, [AsParameters] TRequest request) => await HandleVoidResult(mediator, request));
    }

    public static RouteHandlerBuilder MediatrDelete<TRequest>(this RouteGroupBuilder group, string template)
        where TRequest : IHttpRequest
    {
        return group.MapDelete(template,
            async (IMediator mediator, [AsParameters] TRequest request) => await HandleVoidResult(mediator, request));
    }


    private static async Task<Results<ValidationProblem, Ok<TResponse>, NotFound, ProblemHttpResult>>
        HandleResult<TRequest, TResponse>(ISender mediator, TRequest request)
        where TRequest : IHttpRequest<TResponse>
        where TResponse : class
    {
        var results = await mediator.Send(request);

        if (results.IsSuccess)
        {
            return TypedResults.Ok(results.Value);
        }

        var error = results.Errors.FirstOrDefault();

        return error switch
        {
            ValidationError validationError => TypedResults.ValidationProblem(
                validationError.Failures
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            ),
            NotFoundError notFound => TypedResults.NotFound(),
            _ => TypedResults.Problem(detail: error?.Message ?? "An error has ocurred.")
        };
    }

    private static async Task<Results<Ok, ValidationProblem, NotFound, ProblemHttpResult>> HandleVoidResult<TRequest>(
        ISender mediator, TRequest request) where TRequest : IHttpRequest
    {
        var results = await mediator.Send(request);

        if (results.IsSuccess)
        {
            return TypedResults.Ok();
        }

        var error = results.Errors.FirstOrDefault();

        return error switch
        {
            ValidationError validationError => TypedResults.ValidationProblem(
                validationError.Failures
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            ),
            NotFoundError notFound => TypedResults.NotFound(),
            _ => TypedResults.Problem(detail: error?.Message ?? "An error has ocurred.")
        };
    }
}