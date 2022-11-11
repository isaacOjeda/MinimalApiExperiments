using ApplicationCore.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MinimalApiExperiments.ApplicationCore.Common;
using MinimalApiExperiments.ApplicationCore.Infrastructure.Persistence;

namespace MinimalApiExperiments.ApplicationCore.Features.Products.Commands;
public class CreateProduct : IHttpRequest
{
    public Body Product { get; set; } = default!;

    public class Body
    {
        public string Description { get; set; } = default!;
        public double Price { get; set; }
    }
}

public class CreateProductHandler : IRequestHandler<CreateProduct, IResult>
{
    private readonly AppDbContext _context;

    public CreateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = request.Product.Description,
            Price = request.Product.Price
        };

        _context.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return Results.Created($"api/products/{newProduct.ProductId}", newProduct);
    }
}

public class CreateProductValidator : AbstractValidator<CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(r => r.Product.Description)
            .NotNull();

        RuleFor(r => r.Product.Price)
            .GreaterThan(0)
            .NotNull();
    }
}