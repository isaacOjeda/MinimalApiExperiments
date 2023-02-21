using FluentResults;
using MediatR;

namespace VerticalSliceArchitecture.Core.Common.Interfaces;

public interface IHttpRequest<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IHttpRequest : IRequest<Result>
{
}