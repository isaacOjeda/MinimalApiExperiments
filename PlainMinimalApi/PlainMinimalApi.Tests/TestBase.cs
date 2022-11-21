
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PlainMinimalApi.Infrastructure.Persistence;
using Respawn;
using Respawn.Graph;

namespace PlainMinimalApi.Tests;
public class TestBase
{
    protected PlainMinimalApiApplication Application;


    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Application = new PlainMinimalApiApplication();

        using var scope = Application.Services.CreateScope();

        EnsureDatabase(scope);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        Application.Dispose();
    }

    public async Task<T> AddEntity<T>(T entity)
    {
        using var scope = Application.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Add(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    [SetUp]
    public async Task Setup()
    {
        await ResetState();
    }

    [TearDown]
    public void Down()
    {

    }


    private static void EnsureDatabase(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }


    private static async Task ResetState()
    {
        var checkpoint = new Checkpoint
        {
            TablesToIgnore = new Table[]
            {
                new Table("__EFMigrationsHistory")
            }
        };

        await checkpoint.Reset(PlainMinimalApiApplication.TestConnectionString);
    }
}
