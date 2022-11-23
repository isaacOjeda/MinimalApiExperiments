using VerticalSliceArchitecture.Common.Extensions;
using VerticalSliceArchitecture.Core.Domain.Entities;
using VerticalSliceArchitecture.Core.Features.Products.Commands;
using VerticalSliceArchitecture.Core.Features.Products.Queries;

namespace VerticalSliceArchitecture.Modules;
public static class ProductsModule
{
    public static RouteGroupBuilder AddProductsModule(this RouteGroupBuilder group)
    {
        group.MediatrGet<GetProducts, List<GetProductsResponse>>("/");
        group.MediatrGet<GetProduct, GetProductResponse>("/{ProductId}");
        group.MediatrPost<CreateProduct>("/");
        group.MediatrPut<UpdateProduct>("/");
        group.MediatrDelete<DeleteProduct>("/{ProductId}");
        group
            .WithTags(new string[] { nameof(Product) })
            .WithOpenApi();

        return group;
    }
}
