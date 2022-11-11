using MediatR;
using Microsoft.AspNetCore.Http;

namespace MinimalApiExperiments.ApplicationCore.Common.Interfaces;
public interface IHttpRequest : IRequest<IResult>
{
}