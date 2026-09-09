using ExcelMerger.Models;
using ExcelMerger.Data;
using Microsoft.EntityFrameworkCore;

namespace ExcelMerger.Service;

public class ProductService
{
    private readonly AppDbContext db;

    public ProductService(AppDbContext db)
    {
        this.db = db;
    }
    
    public void AddProduct(Product product)
    {
        db.Products.Add(product);
        db.SaveChanges();
    }

    public bool DoesProductExist(string name)
    {
        return db.Products.Any(p => p.Name == name);
    }

    public IReadOnlyList<Product> GetProducts()
    {
        return db.Products.ToList();
    }

    public Product? GetProductById(Guid id)
    {
        return db.Products.FirstOrDefault(p => p.Id == id);
    }

}