﻿using ApplicationCore.Common;
using ApplicationCore.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Features.Products.Commands;
public class UpdateProduct : IHttpRequest
{
    public Body Product { get; set; } = default!;

    public class Body
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}


public class UpdateProductHandler : IRequestHandler<UpdateProduct, IResult>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(UpdateProduct request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Product.ProductId);

        if (product is null)
        {
            return Results.NotFound();
        }


        product.Description = request.Product.Description;
        product.Price = request.Product.Price;

        await _context.SaveChangesAsync(cancellationToken);

        return Results.Ok();
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