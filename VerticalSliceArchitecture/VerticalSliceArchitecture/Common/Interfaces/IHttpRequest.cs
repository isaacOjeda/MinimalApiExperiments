using MediatR;
using Microsoft.AspNetCore.Http;

namespace VerticalSliceArchitecture.Common.Interfaces;
public interface IHttpRequest : IRequest<IResult>
{
}