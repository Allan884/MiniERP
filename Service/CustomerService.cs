using ExcelMerger.Data;
using Microsoft.EntityFrameworkCore;

namespace ExcelMerger.Models;

public class CustomerService
{
    private readonly AppDbContext db;

    public CustomerService(AppDbContext db)
    {
        this.db = db;
    }

    public void AddCustomer(Customer customer)
    {
        db.Customers.Add(customer);
        db.SaveChanges();
       
    }

    public bool DoesCustomerExist(string name)
    {
        return db.Customers.Any(c => c.Name == name);
    }

    public bool DoesCustomerExist(Guid id)
    {
        return db.Customers.Any(c => c.Id == id);
    }

    public IReadOnlyList<Customer> GetCustomers()
    {
        return db.Customers.ToList();
    }

    public Customer? GetCustomerById(Guid id)
    {
        return db.Customers.FirstOrDefault(c => c.Id == id);
    }

    public Customer? GetCustomerByName(string name)
    {
        return db.Customers.FirstOrDefault(c => c.Name == name);
    }

    public void ChangeCustomerName (Guid customerId, string newName)
    {
        var customer = GetCustomerById(customerId)
            ?? throw new InvalidOperationException("Customer was not found");

        customer.ChangeName(newName);

        db.SaveChanges();
    }

}