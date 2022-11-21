using FluentAssertions;
using NUnit.Framework;
using PlainMinimalApi.Domain.Entities;
using PlainMinimalApi.Features.Products.Commands;
using PlainMinimalApi.Features.Products.Queries;
using System.Net;
using System.Net.Http.Json;

namespace PlainMinimalApi.Tests.Features;


public class ProductsTests : TestBase
{

    [Test]
    public async Task GetProducts()
    {
        // Arrenge
        var category = await AddEntity(new Category
        {
            Description = "Description Test"
        });

        await AddEntity(new Product
        {
            Description = "Test",
            Price = 999,
            CategoryId = category.CategoryId
        });

        var http = Application.CreateDefaultClient();

        // Act

        var products = await http.GetFromJsonAsync<List<GetProductsResponse>>("/api/products");

        // Assert
        products.Count.Should().Be(1);

    }

    [Test]
    public async Task GetEmptyProducts()
    {
        // Arrenge
        var http = Application.CreateDefaultClient();

        // Act
        var products = await http.GetFromJsonAsync<List<GetProductsResponse>>("api/products");

        // Assert
        products.Count().Should().Be(0);
    }


    [Test]
    public async Task CreateProductSuccess()
    {
        // Arrenge
        var http = Application.CreateDefaultClient();
        var category = await AddEntity(new Category
        {
            Description = "Description Test"
        });
        var newProduct = new CreateProductRequest
        {
            Description = $"Test_{Guid.NewGuid()}",
            Price = 999,
            CategoryId = category.CategoryId
        };

        // Act
        var result = await http.PostAsJsonAsync("api/products", newProduct);

        // Assert
        result.IsSuccessStatusCode.Should().BeTrue();
        var exists = await FindEntity<Product>(q => q.Description == newProduct.Description);

        exists.Should().NotBeNull();
        exists.Description.Should().Be(newProduct.Description);
    }

    [Test]
    public async Task CreateProductValidationFail()
    {
        // Arrenge
        var http = Application.CreateDefaultClient();
        var newProduct = new CreateProductRequest
        {
            Description = "Test",
            Price = 0
        };

        // Act
        var result = await http.PostAsJsonAsync("api/products", newProduct);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
}