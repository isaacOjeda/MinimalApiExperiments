using ApplicationCore.Common.Extensions;
using ApplicationCore.Features.Products.Commands;
using ApplicationCore.Features.Products.Queries;
using Microsoft.AspNetCore.Routing;

namespace ApplicationCore.Features.Products;
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
