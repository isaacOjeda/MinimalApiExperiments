namespace PlainMinimalApi.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Description { get; set; } = default!;

    public ICollection<Product> Products { get; set; } =
        new HashSet<Product>();
}