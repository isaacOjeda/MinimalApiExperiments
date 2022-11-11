using MediatR;
using Microsoft.AspNetCore.Http;

namespace VerticalSliceArchitecture.ApplicationCore.Common.Interfaces;
public interface IHttpRequest : IRequest<IResult>
{
}