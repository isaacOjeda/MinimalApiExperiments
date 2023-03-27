
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PlainMinimalApi.Infrastructure.Persistence;
using Respawn;
using Respawn.Graph;
using System.Linq.Expressions;

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

    public async Task<T> FindEntity<T>(Expression<Func<T, bool>> predicate)
        where T : class
    {
        using var scope = Application.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Set<T>().FirstOrDefaultAsync(predicate);
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
        var respawner = await Respawner.CreateAsync(PlainMinimalApiApplication.TestConnectionString, new RespawnerOptions
        {
            TablesToIgnore = new Table[]
            {
                "__EFMigrationsHistory"
            }
        });

        await respawner.ResetAsync(PlainMinimalApiApplication.TestConnectionString);
    }
}
