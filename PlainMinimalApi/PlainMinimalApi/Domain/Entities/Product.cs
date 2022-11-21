namespace PlainMinimalApi.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Description { get; set; } = default!;
    public double Price { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
