using ApplicationCore.Common;
using ApplicationCore.Domain.Entities;
using ApplicationCore.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Features.Products.Commands;
public class CreateProduct : IHttpRequest
{
    public Body Data { get; set; }

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
            Description = request.Data.Description,
            Price = request.Data.Price
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
        RuleFor(r => r.Data.Description)
            .NotNull();

        RuleFor(r => r.Data.Price)
            .NotNull();
    }
}