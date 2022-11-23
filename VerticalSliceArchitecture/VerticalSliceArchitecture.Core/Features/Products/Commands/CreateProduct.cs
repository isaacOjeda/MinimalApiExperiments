
using FluentResults;
using FluentValidation;
using MediatR;
using VerticalSliceArchitecture.Core.Common.Interfaces;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Infrastructure.Persistence;


namespace VerticalSliceArchitecture.Core.Features.Products.Commands;
public class CreateProduct : IHttpRequest
{
    public CreateProductBody Product { get; set; } = default!;

    public class CreateProductBody
    {
        public string Description { get; set; } = default!;
        public double Price { get; set; }
    }
}

public class CreateProductHandler : IRequestHandler<CreateProduct, Result>
{
    private readonly AppDbContext _context;

    public CreateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = request.Product.Description,
            Price = request.Product.Price
        };

        _context.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
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