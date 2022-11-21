using PlainMinimalApi.Common;
using PlainMinimalApi.Domain.Entities;
using PlainMinimalApi.Features.Products.Commands;
using PlainMinimalApi.Features.Products.Queries;

namespace PlainMinimalApi.Features.Products;
public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProducts(this WebApplication app)
    {
        var group = app.MapGroup("api/products");

        group.MapGet("/", GetProducts.Handle)
            .WithName(nameof(GetProducts));

        group.MapPost("/", CreateProduct.Handle)
            .WithName(nameof(CreateProduct));


        group.MapGet("/{id}", GetProductById.Handle)
            .WithName(nameof(GetProductById));


        group.WithTags(new string[] { nameof(Product) });
        group.WithOpenApi();

        group.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

        return group;
    }
}
