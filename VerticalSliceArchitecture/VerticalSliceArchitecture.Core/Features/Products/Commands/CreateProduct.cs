using FluentResults;
using FluentValidation;
using MediatR;
using VerticalSliceArchitecture.Core.Common.Interfaces;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Infrastructure.Persistence;


namespace VerticalSliceArchitecture.Core.Features.Products.Commands;

public class CreateProduct : IHttpRequest<CreateProductResponse>
{
    public CreateProductBody Product { get; set; } = default!;

    public class CreateProductBody
    {
        public string Description { get; set; } = default!;
        public double Price { get; set; }
    }
}

public class CreateProductHandler : IRequestHandler<CreateProduct, Result<CreateProductResponse>>
{
    private readonly AppDbContext _context;

    public CreateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = request.Product.Description,
            Price = request.Product.Price
        };

        _context.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateProductResponse { ProductId = newProduct.ProductId });
    }
}

public class CreateProductResponse
{
    public int ProductId { get; set; }
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