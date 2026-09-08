using ExcelMerger.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelMerger.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<SalesOrderLine> SalesOrderLines { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Customer>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<SalesOrder>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<SalesOrder>()
            .Property(s => s.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<SalesOrder>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(s => s.CustomerId);

        modelBuilder.Entity<SalesOrderLine>()
            .HasOne<SalesOrder>()
            .WithMany(order => order.SalesOrderLines)
            .HasForeignKey(line => line.SalesOrderId);

        modelBuilder.Entity<SalesOrderLine>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(line => line.ProductId);
    }

}