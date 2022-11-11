using Microsoft.EntityFrameworkCore;
using MinimalApiExperiments.ApplicationCore.Domain.Entities;

namespace MinimalApiExperiments.ApplicationCore.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
}
