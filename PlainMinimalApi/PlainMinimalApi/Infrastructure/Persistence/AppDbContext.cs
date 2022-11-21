using Microsoft.EntityFrameworkCore;
using PlainMinimalApi.Domain.Entities;

namespace PlainMinimalApi.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
}
