using Microsoft.EntityFrameworkCore;
using PlainMinimalApi.Infrastructure.Persistence;

namespace PlainMinimalApi.Features.Products.Queries;

public static class GetProducts
{
    public static async Task<List<GetProductsResponse>> Handle(AppDbContext context)
    {
        return await context.Products
            .Select(s => new GetProductsResponse
            {
                Description = s.Description,
                Price = s.Price,
                ProductId = s.ProductId
            })
            .ToListAsync();
    }
}

public class GetProductsResponse
{
    public int ProductId { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
