using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.ApplicationCore.Domain.Entities;

namespace VerticalSliceArchitecture.ApplicationCore.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
}
