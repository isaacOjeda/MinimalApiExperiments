using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Core.Common.Interfaces;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Core.Features.Products.Queries;

public class GetProducts : IHttpRequest<List<GetProductsResponse>>
{

}

public class GetProductsHandler : IRequestHandler<GetProducts, Result<List<GetProductsResponse>>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<GetProductsResponse>>> Handle(GetProducts request, CancellationToken cancellationToken) =>
        Result.Ok(
            await _context.Products
                .ProjectTo<GetProductsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync()
        );
}

public class GetProductsResponse
{
    public int ProductId { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}

public class GetProductsProfile : Profile
{
    public GetProductsProfile()
    {
        CreateMap<Product, GetProductsResponse>();
    }
}