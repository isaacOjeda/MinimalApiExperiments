using ApplicationCore;
using ApplicationCore.Features.Products;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationCore();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello Minimal API Experiments!");

app.MapGroup("api/products")
    .AddProductsModule();

app.Run();
