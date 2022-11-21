using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlainMinimalApi.Domain.Entities;

namespace PlainMinimalApi.Infrastructure.Persistence.Configurations;


public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(q => q.Category)
            .WithMany(q => q.Products)
            .HasForeignKey(q => q.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}