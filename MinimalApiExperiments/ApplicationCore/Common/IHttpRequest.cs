using MediatR;
using Microsoft.AspNetCore.Http;

namespace MinimalApiExperiments.ApplicationCore.Common;
public interface IHttpRequest : IRequest<IResult>
{
}