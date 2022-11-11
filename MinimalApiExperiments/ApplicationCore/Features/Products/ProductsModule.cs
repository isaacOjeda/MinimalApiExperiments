﻿using Microsoft.AspNetCore.Routing;
using MinimalApiExperiments.ApplicationCore.Common.Extensions;
using MinimalApiExperiments.ApplicationCore.Features.Products.Commands;
using MinimalApiExperiments.ApplicationCore.Features.Products.Queries;

namespace MinimalApiExperiments.ApplicationCore.Features.Products;
public static class ProductsModule
{
    public static RouteGroupBuilder AddProductsModule(this RouteGroupBuilder group)
    {
        group.MediatrGet<GetProducts>("/");
        group.MediatrGet<GetProduct>("/{ProductId}");
        group.MediatrPost<CreateProduct>("/");
        group.MediatrPut<UpdateProduct>("/");
        group.MediatrDelete<DeleteProduct>("/{ProductId}");

        return group;
    }
}
