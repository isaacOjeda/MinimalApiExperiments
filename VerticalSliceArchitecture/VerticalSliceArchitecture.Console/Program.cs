using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using VerticalSliceArchitecture.Core;
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


var sender = serviceCollection.GetRequiredService<ISender>();

AnsiConsole.MarkupLine("[b]Loading Products from database [/]");

var queryResult = await sender.Send(new GetProducts());


if (queryResult.IsSuccess)
{
    AnsiConsole.MarkupLine("[b green]Done. [/]");
    foreach (var product in queryResult.Value)
    {
        AnsiConsole.WriteLine("Description: {0} - Price: {1:c}", product.Description, product.Price);
    }
}


