using FluentResults;
using FluentValidation;
using MediatR;
using VerticalSliceArchitecture.Core.Common.Errors;
using VerticalSliceArchitecture.Core.Common.Interfaces;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Core.Features.Products.Commands;
public class UpdateProduct : IHttpRequest
{
    public UpdateProductBody Product { get; set; } = default!;

    public class UpdateProductBody
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}


public class UpdateProductHandler : IRequestHandler<UpdateProduct, Result>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateProduct request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Product.ProductId);

        if (product is null)
        {
            return Result.Fail(NotFoundError.Create(nameof(Product)));
        }


        product.Description = request.Product.Description;
        product.Price = request.Product.Price;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

public class UpdateProductValidator : AbstractValidator<UpdateProduct>
{
    public UpdateProductValidator()
    {
        RuleFor(r => r.Product.ProductId)
            .NotNull();

        RuleFor(r => r.Product.Price)
            .GreaterThan(0)
            .NotNull();

        RuleFor(r => r.Product.Description)
            .NotNull();
    }
}