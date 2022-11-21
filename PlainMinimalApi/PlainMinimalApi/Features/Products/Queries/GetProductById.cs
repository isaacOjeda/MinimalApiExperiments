using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PlainMinimalApi.Infrastructure.Persistence;

namespace PlainMinimalApi.Features.Products.Queries;
public static class GetProductById
{
    public static async Task<Results<Ok<GetProductResponse>, NotFound>> Handle(AppDbContext context, int id)
    {
        var product = await context.Products
            .Select(s => new GetProductResponse
            {
                ProductId = s.ProductId,
                Category = s.Category.Description,
                Description = s.Description,
                Price = s.Price,
            })
            .FirstOrDefaultAsync(q => q.ProductId == id);


        if (product is null)
        {
            return TypedResults.NotFound();
        }


        return TypedResults.Ok(product);
    }
}

public class GetProductResponse
{
    public int ProductId { get; set; }
    public string Description { get; set; } = default!;
    public double Price { get; set; }
    public string Category { get; set; } = default!;
}
