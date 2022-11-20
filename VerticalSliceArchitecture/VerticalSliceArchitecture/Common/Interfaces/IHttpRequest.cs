using MediatR;

namespace VerticalSliceArchitecture.Common.Interfaces;
public interface IHttpRequest : IRequest<IResult>
{
}