using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using VerticalSliceArchitecture.Core;
using VerticalSliceArchitecture.Core.Common.Errors;
using VerticalSliceArchitecture.Core.Features.Products.Queries;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
    .AddJsonFile("appsettings.json", false)
    .Build();


var services = new ServiceCollection()
    .AddApplicationCore()
    .AddPersistence(configuration);

var serviceCollection = services.BuildServiceProvider();


AnsiConsole.Write(
    new FigletText("Vertical Slice Architecture CLI")
        .Centered()
        .Color(Color.Orange3)
    );


var productIdToSearch = AnsiConsole.Ask<int>("Buscar productos por ID");
var sender = serviceCollection.GetRequiredService<ISender>();

var queryResult = await sender.Send(new GetProduct
{
    ProductId = productIdToSearch
});


if (queryResult.IsSuccess)
{
    var product = queryResult.Value;
    AnsiConsole.WriteLine($"El precio de {product.Description} es {product.Price:c}");
}
else
{
    var firstError = queryResult.Errors.FirstOrDefault();

    var message = firstError switch
    {
        NotFoundError notfound => $"[b red]Producto no encontrado[/] \n {notfound.Message}",
        _ => "[b red]Ocurrió un error no esperado[/]"
    };

    AnsiConsole.MarkupLine(message);
}

//var sender = serviceCollection.GetRequiredService<ISender>();

//AnsiConsole.MarkupLine("[b]Loading Products from database [/]");

//var queryResult = await sender.Send(new GetProducts());


//if (queryResult.IsSuccess)
//{
//    AnsiConsole.MarkupLine("[b green]Done. [/]");
//    var grid = new Grid();

//    // Add columns 
//    grid.AddColumn();
//    grid.AddColumn();

//    // Add header row 
//    grid.AddRow(new string[] { "Description", "Price" });

//    // Write to Console
//    foreach (var product in queryResult.Value)
//    {
//        grid.AddRow(new string[] { product.Description, product.Price.ToString("c") });
//    }

//    AnsiConsole.Write(grid);
//}


