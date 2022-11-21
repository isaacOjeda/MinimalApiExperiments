using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PlainMinimalApi.Domain.Entities;
using PlainMinimalApi.Infrastructure.Persistence;

namespace PlainMinimalApi.Features.Products.Commands;


public class CreateProductRequest
{
    public double Price { get; set; }
    public string Description { get; set; } = default!;
}

public static class CreateProduct
{
    public static async Task<Ok> Handle(AppDbContext context, [Validate] CreateProductRequest request)
    {
        var newProduct = new Product
        {
            Price = request.Price,
            Description = request.Description
        };

        context.Add(newProduct);


        await context.SaveChangesAsync();

        return TypedResults.Ok();
    }
}


public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.Price).GreaterThan(0);
    }
}