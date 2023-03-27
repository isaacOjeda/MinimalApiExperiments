using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlainMinimalApi.Infrastructure.Persistence;

namespace PlainMinimalApi.Tests;

public class PlainMinimalApiApplication : WebApplicationFactory<Api>
{
    public const string TestConnectionString = "Server=(localdb)\\mssqllocaldb;Database=PlanMinimalApi_TestDb;Trusted_Connection=True;MultipleActiveResultSets=false";


    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                // Usamos una LocalDB para pruebas de integración
                return new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(TestConnectionString)
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });
        });

        return base.CreateHost(builder);
    }
}
