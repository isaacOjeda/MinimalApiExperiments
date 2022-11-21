﻿using PlainMinimalApi.Common;
using PlainMinimalApi.Domain.Entities;
using PlainMinimalApi.Features.Products.Commands;
using PlainMinimalApi.Features.Products.Queries;

namespace PlainMinimalApi.Features.Products;
public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProducts(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetProducts.Handle)
            .WithName(nameof(GetProducts));

        group.MapPost("/", CreateProduct.Handle)
            .WithName(nameof(CreateProduct));


        group.WithTags(new string[] { nameof(Product) });
        group.WithOpenApi();

        group.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

        return group;
    }
}
