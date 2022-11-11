﻿using MinimalApiExperiments.ApplicationCore.Domain.Entities;
using MinimalApiExperiments.ApplicationCore.Features.Products.Commands;
using MinimalApiExperiments.ApplicationCore.Features.Products.Queries;
using MinimalApiExperiments.Common.Extensions;

namespace MinimalApiExperiments.Modules;
public static class ProductsModule
{
    public static RouteGroupBuilder AddProductsModule(this RouteGroupBuilder group)
    {
        group.MediatrGet<GetProducts>("/")
            .Produces<GetProductsResponse>();

        group.MediatrGet<GetProduct>("/{ProductId}")
            .Produces<GetProductResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MediatrPost<CreateProduct>("/")
            .ProducesValidationProblem()
            .Produces<Product>(StatusCodes.Status201Created);

        group.MediatrPut<UpdateProduct>("/")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status200OK);

        group.MediatrDelete<DeleteProduct>("/{ProductId}")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        group
            .WithTags(new string[] { nameof(Product) })
            .WithOpenApi();

        return group;
    }
}
