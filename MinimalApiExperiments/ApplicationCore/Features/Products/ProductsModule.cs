using ApplicationCore.Common.Extensions;
using ApplicationCore.Domain.Entities;
using ApplicationCore.Features.Products.Commands;
using ApplicationCore.Features.Products.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace ApplicationCore.Features.Products;
public static class ProductsModule
{
    public static RouteGroupBuilder AddProductsModule(this RouteGroupBuilder group)
    {
        group.MediatrGet<GetProducts, List<GetProductsResponse>>("/");
        group.MediatrPost<CreateProduct, Created<Product>>("/");

        return group;
    }
}
