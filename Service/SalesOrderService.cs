using ExcelMerger.Data;
using ExcelMerger.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelMerger.Service;

public class SalesOrderService
{
    private readonly AppDbContext db;
    private readonly ProductService productService;
    private readonly CustomerService customerService;

    
    public SalesOrderService(AppDbContext db, CustomerService customerService, ProductService productService)
    {
        this.db = db;
        this.customerService = customerService;
        this.productService = productService;
    }

    public void AddSalesOrder(SalesOrder salesOrder)
    {
        if (salesOrder == null)
        {
            throw new ArgumentNullException(nameof(salesOrder));
        }

    db.SalesOrders.Add(salesOrder);
    db.SaveChanges();

    }

    public IReadOnlyList<SalesOrder> GetSalesOrders()
    {
        return db.SalesOrders.ToList();
    }

    public SalesOrder CreateSalesOrder(Guid CustomerId)
    {

        var customer = customerService.GetCustomerById(CustomerId);

        if (customer == null)
        {
            throw new ArgumentException("Customer does not exist.");
        }


        string orderNumber = GenerateOrderNumber();

        var salesOrder = new SalesOrder(customer.Id, orderNumber);

        
        AddSalesOrder(salesOrder);

        return salesOrder;
    }

   
    private string GenerateOrderNumber()
    {
        int maxNumber = 0;

        var orders = db.SalesOrders.ToList();

        foreach (var order in orders)
        {
            string numberPart = order.OrderNumber.Replace("SO-", "");
            int number = int.Parse(numberPart);

            if (number > maxNumber)
            {
            maxNumber = number;
            }
        }

        int nextNumber = maxNumber + 1;

        return $"SO-{nextNumber:D3}";
    }

    public void AddLineToSalesOrder(string orderNumber, Guid productId, int quantity, DateOnly deliveryDate)
    {
        var order = GetOrderByNumber(orderNumber);
        if (order == null)
        {
            throw new ArgumentException("Order does not exist.");
        }
        
        var product = productService.GetProductById(productId);

        if (product == null)
        {
            throw new ArgumentException("Product does not exist.");
        }

        var line = new SalesOrderLine(product.Id, order.Id, product.Name, quantity, product.Price, deliveryDate);
        order.AddLine(line);
        db.SalesOrderLines.Add(line);
        db.SaveChanges();
    }

    public SalesOrder? GetOrderByNumber(string orderNumber)
    {
        return db.SalesOrders
        .Include(o => o.SalesOrderLines)
        .FirstOrDefault(o => o.OrderNumber == orderNumber);
    }

    public decimal GetOrderTotalPrice(string orderNumber)
    {
        var order = GetOrderByNumber(orderNumber);
        if (order == null)
        {
            throw new ArgumentException("Order does not exist.");
        }
        
        return order.GetOrderTotalPrice();
    }

}