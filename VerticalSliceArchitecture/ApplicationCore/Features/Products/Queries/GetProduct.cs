using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.ApplicationCore.Common.Interfaces;
using VerticalSliceArchitecture.ApplicationCore.Domain.Entities;
using VerticalSliceArchitecture.ApplicationCore.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.ApplicationCore.Features.Products.Queries;
public class GetProduct : IHttpRequest
{
    public int ProductId { get; set; }
}

public class GetProductHandler : IRequestHandler<GetProduct, IResult>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IResult> Handle(GetProduct request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .ProjectTo<GetProductResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.ProductId == request.ProductId);

        if (product is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(product);
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