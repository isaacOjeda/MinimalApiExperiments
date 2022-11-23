using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Core.Domain.Entities;

namespace VerticalSliceArchitecture.Core.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
}
