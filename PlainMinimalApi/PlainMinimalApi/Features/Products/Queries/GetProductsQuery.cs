using Microsoft.EntityFrameworkCore;
using PlainMinimalApi.Infrastructure.Persistence;

namespace PlainMinimalApi.Features.Products.Queries;

public static class GetProductsHandler
{
    public static async Task<List<GetProductsResponse>> Handler(AppDbContext context)
    {
        return await context.Products
            .Select(s => new GetProductsResponse
            {
                Description = s.Description,
                Price = s.Price,
                ProductId = s.ProductId,
                Category = s.Category.Description
            })
            .ToListAsync();
    }
}

public class GetProductsResponse
{
    public int ProductId { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public double Price { get; set; }
}
