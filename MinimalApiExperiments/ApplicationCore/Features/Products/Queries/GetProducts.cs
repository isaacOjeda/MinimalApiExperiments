using ApplicationCore.Domain.Entities;
using ApplicationCore.Infrastructure.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Features.Products.Queries;

public class GetProducts : IRequest<List<GetProductsResponse>>
{

}

public class GetProductsHandler : IRequestHandler<GetProducts, List<GetProductsResponse>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetProductsResponse>> Handle(GetProducts request, CancellationToken cancellationToken) =>
        _context.Products
            .ProjectTo<GetProductsResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
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