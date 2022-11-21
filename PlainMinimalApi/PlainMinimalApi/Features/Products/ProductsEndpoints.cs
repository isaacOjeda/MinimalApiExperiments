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

        group.MapGet("/", GetProductsHandler.Handler)
            .WithName("GetProducts");

        group.MapPost("/", CreateProductHandler.Handler)
            .WithName("CreateProduct");


        group.MapGet("/{id}", GetProductHandler.Handler)
            .WithName("GetProduct");


        group.WithTags(new string[] { nameof(Product) });
        group.WithOpenApi();

        group.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

        return group;
    }
}
