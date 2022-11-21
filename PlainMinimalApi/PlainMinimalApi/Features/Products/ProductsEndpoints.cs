﻿using PlainMinimalApi.Common;
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
            .WithName(nameof(GetProductsHandler));

        group.MapPost("/", CreateProductHandler.Handler)
            .WithName(nameof(CreateProductHandler));


        group.MapGet("/{id}", GetProductHandler.Handler)
            .WithName(nameof(GetProductHandler));


        group.WithTags(new string[] { nameof(Product) });
        group.WithOpenApi();

        group.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

        return group;
    }
}
