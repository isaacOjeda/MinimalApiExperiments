using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PlainMinimalApi.Domain.Entities;
using PlainMinimalApi.Infrastructure.Persistence;

namespace PlainMinimalApi.Features.Products.Commands;


public class CreateProductCommand
{
    public double Price { get; set; }
    public string Description { get; set; } = default!;
    public int CategoryId { get; set; }
}

public static class CreateProductHandler
{
    public static async Task<Ok> Handler(AppDbContext context, [Validate] CreateProductCommand request)
    {
        var newProduct = new Product
        {
            Price = request.Price,
            Description = request.Description,
            CategoryId = request.CategoryId
        };

        context.Add(newProduct);


        await context.SaveChangesAsync();

        return TypedResults.Ok();
    }
}


public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.Price).GreaterThan(0);
        RuleFor(r => r.CategoryId).GreaterThan(0);
    }
}