namespace ExcelMerger.Models;
public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Address { get; private set; }
    public string? ContactPerson { get; private set; }


    public Customer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Customer name is required");
        }

        Id = Guid.NewGuid();
        Name = name;
        Address = null;
        ContactPerson = null;
    }

    public void ChangeName (string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Customer name is required");
        }

        Name = newName;
    }
}