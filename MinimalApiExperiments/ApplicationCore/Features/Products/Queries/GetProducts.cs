using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalApiExperiments.ApplicationCore.Common;
using MinimalApiExperiments.ApplicationCore.Domain.Entities;
using MinimalApiExperiments.ApplicationCore.Infrastructure.Persistence;

namespace MinimalApiExperiments.ApplicationCore.Features.Products.Queries;

public class GetProducts : IHttpRequest
{

}

public class GetProductsHandler : IRequestHandler<GetProducts, IResult>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IResult> Handle(GetProducts request, CancellationToken cancellationToken) =>
        Results.Ok(
            await _context.Products
                .ProjectTo<GetProductsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync());
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