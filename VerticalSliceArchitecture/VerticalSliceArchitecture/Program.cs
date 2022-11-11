using VerticalSliceArchitecture.ApplicationCore;
using VerticalSliceArchitecture.Modules;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationCore();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapGet("/", () => "Hello Minimal API Experiments!");

app.MapGroup("api/products")
    .AddProductsModule();

app.Run();