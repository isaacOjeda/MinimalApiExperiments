using FluentResults;
using MediatR;
using VerticalSliceArchitecture.Core.Common.Errors;
using VerticalSliceArchitecture.Core.Common.Interfaces;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Infrastructure.Persistence;

namespace VerticalSliceArchitecture.Core.Features.Products.Commands;

public class DeleteProduct : IHttpRequest
{
    public int ProductId { get; set; }
}

public class DeleteProductHandler : IRequestHandler<DeleteProduct, Result>
{
    private readonly AppDbContext _context;

    public DeleteProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId);

        if (product is null)
        {
            return Result.Fail(new NotFoundError(nameof(Product)));
        }

        _context.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}