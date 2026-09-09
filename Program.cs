using ExcelMerger.Models;
using ExcelMerger.Data;
using ExcelMerger.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString =
    configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql(connectionString)
    .Options;

using var db = new AppDbContext(options);
Console.WriteLine($"Database: {db.Database.GetDbConnection().Database}");

Customer customer = new Customer("Acme Corporation");
CustomerService customerService = new CustomerService(db);
ProductService productService = new ProductService(db);

Product product = new Product("Widget", 9.99m);
productService.AddProduct(product);

bool exists = customerService.DoesCustomerExist(customer.Name);

    if (exists)
    {
        Console.WriteLine("Customer already exists. Are you sure to add it again? (yes/no)");

        var answer = Console.ReadLine();

        if (answer == "yes")
        {
            customerService.AddCustomer(customer);
            Console.WriteLine("Customer added successfully.");
        }
        else
        {
            customer = customerService.GetCustomerByName(customer.Name) 
            ?? throw new InvalidOperationException("Customer was not found.");
        }
    }
    else
    {
        customerService.AddCustomer(customer); Console.WriteLine("Customer added successfully.");
    }

SalesOrderService salesOrderService =
    new SalesOrderService(db, customerService, productService);

    
SalesOrder salesOrder = salesOrderService.CreateSalesOrder(customer.Id);
Console.WriteLine("SalesOrder created.");

salesOrderService.AddLineToSalesOrder(salesOrder.OrderNumber, product.Id, 5, DateOnly.FromDateTime(DateTime.Now.AddDays(7)));
Console.WriteLine("SalesOrderLine added.");
salesOrderService.AddLineToSalesOrder(salesOrder.OrderNumber, product.Id, 5, DateOnly.FromDateTime(DateTime.Now.AddDays(7)));
Console.WriteLine("SalesOrderLine added.");
Console.WriteLine(salesOrderService.GetOrderTotalPrice(salesOrder.OrderNumber));

Console.WriteLine(salesOrderService.GetOrderByNumber(salesOrder.OrderNumber)?.SalesOrderLines.Count);

try
{
    Customer customer1 = new Customer("Acme Corporation");
    customerService.AddCustomer(customer1);
    Customer customer2 = new Customer("Acme Corporation");
    customerService.AddCustomer(customer2);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
