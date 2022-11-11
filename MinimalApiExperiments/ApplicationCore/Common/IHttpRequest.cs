using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Common;
public interface IHttpRequest : IRequest<IResult>
{
}
