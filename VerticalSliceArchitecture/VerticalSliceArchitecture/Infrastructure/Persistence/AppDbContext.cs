using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Domain.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
}
