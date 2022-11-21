namespace CleanArchitecture.Core.Domain.Entities;
public class Product
{
    public int ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
}
