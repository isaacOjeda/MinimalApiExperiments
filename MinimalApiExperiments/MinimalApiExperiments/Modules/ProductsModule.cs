﻿using MinimalApiExperiments.ApplicationCore.Features.Products.Commands;
using MinimalApiExperiments.ApplicationCore.Features.Products.Queries;
using MinimalApiExperiments.Common.Extensions;

namespace MinimalApiExperiments.Modules;
public static class ProductsModule
{
    public static RouteGroupBuilder AddProductsModule(this RouteGroupBuilder group)
    {
        group.MediatrGet<GetProducts>("/");
        group.MediatrGet<GetProduct>("/{ProductId}")
            .Produces<GetProductResponse>()
            .Produces(StatusCodes.Status404NotFound);
        group.MediatrPost<CreateProduct>("/");
        group.MediatrPut<UpdateProduct>("/");
        group.MediatrDelete<DeleteProduct>("/{ProductId}");

        return group;
    }
}