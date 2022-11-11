using ApplicationCore.Domain.Entities;
using ApplicationCore.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Features.Products.Commands;
public class CreateProduct : IRequest<Created<Product>>
{
    [FromBody]
    public Body Data { get; set; }

    public class Body
    {
        public string Description { get; set; } = default!;
        public double Price { get; set; }
    }
}

public class CreateProductHandler : IRequestHandler<CreateProduct, Created<Product>>
{
    private readonly AppDbContext _context;

    public CreateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Created<Product>> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = request.Data.Description,
            Price = request.Data.Price
        };

        _context.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return TypedResults.Created($"api/products/{newProduct.ProductId}", newProduct);
    }
}

public class CreateProductValidator : AbstractValidator<CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(r => r.Data.Description)
            .NotNull();

        RuleFor(r => r.Data.Price)
            .NotNull();
    }
}