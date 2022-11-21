using FluentValidation;
using PlainMinimalApi.Domain.Entities;
using PlainMinimalApi.Features.Products;
using PlainMinimalApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGroup("api/products")
    .MapProducts();


await SeedData();

app.Run();




async Task SeedData()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Products.Any())
    {


        context.Add(new Product
        {
            Description = "Samsung TV 01",
            Price = 12599,
            Category = new Category
            {
                Description = "Electronics"
            }
        });

        await context.SaveChangesAsync();
    }
}