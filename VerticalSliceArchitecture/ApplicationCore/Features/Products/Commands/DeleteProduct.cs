﻿using MediatR;
using Microsoft.AspNetCore.Http;
using VerticalSliceArchitecture.ApplicationCore.Common.Interfaces;
using VerticalSliceArchitecture.ApplicationCore.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.ApplicationCore.Features.Products.Commands;
public class DeleteProduct : IHttpRequest
{
    public int ProductId { get; set; }
}

public class DeleteProductHandler : IRequestHandler<DeleteProduct, IResult>
{
    private readonly AppDbContext _context;

    public DeleteProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId);

        if (product is null)
        {
            return Results.NotFound();
        }

        _context.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
}