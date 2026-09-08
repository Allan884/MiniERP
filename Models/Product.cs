namespace ExcelMerger.Models;

public class Product
{
    public Guid Id {get; set; }
    public string Name {get; set; }
    public decimal Price {get; set; }


    public Product(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name is required");
        }

        if (price < 0)
        {
            throw new ArgumentException("Product price cannot be negative");
        }

        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }
}