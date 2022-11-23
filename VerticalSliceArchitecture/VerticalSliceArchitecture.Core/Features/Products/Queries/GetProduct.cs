using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Core.Common.Errors;
using VerticalSliceArchitecture.Core.Common.Interfaces;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Core.Features.Products.Queries;
public class GetProduct : IHttpRequest<GetProductResponse>
{
    public int ProductId { get; set; }
}

public class GetProductHandler : IRequestHandler<GetProduct, Result<GetProductResponse>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<GetProductResponse>> Handle(GetProduct request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .ProjectTo<GetProductResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.ProductId == request.ProductId);

        if (product is null)
        {
            return Result.Fail(NotFoundError.Create(nameof(Product)));
        }


        return Result.Ok(product);
    }

}

public class GetProductResponse
{
    public int ProductId { get; set; }
    public string Description { get; set; } = default!;
    public double Price { get; set; }
}

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResponse>();
    }
}