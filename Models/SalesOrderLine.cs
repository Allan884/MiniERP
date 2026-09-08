public class SalesOrderLine
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } 
    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; } 
    public DateOnly DeliveryDate { get; private set; } 
    public decimal RowTotalPrice => Quantity * UnitPrice;

    public Guid SalesOrderId { get; private set; }

    public SalesOrderLine(Guid productId, Guid salesOrderId, string productName, int quantity, decimal unitPrice, DateOnly deliveryDate)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentException("Unit price must be a non-negative value");
        }

        if (deliveryDate < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Delivery date cannot be in the past");
        }

        Id = Guid.NewGuid();
        SalesOrderId = salesOrderId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        DeliveryDate = deliveryDate;
    }


    public void ChangeDeliveryDate(DateOnly newDeliveryDate)
    {
        if (newDeliveryDate < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Delivery date cannot be in the past");
        }

        DeliveryDate = newDeliveryDate;
    }


    public void ChangeQuantity (int newQuantity)
    {
        if (newQuantity <= 0)
        {
        throw new ArgumentException("Quantity must be greater than zero");
        }

        Quantity = newQuantity;
    }

    public void ChangePrice (decimal newUnitPrice)
    {
        if (newUnitPrice < 0)
        {
        throw new ArgumentException("Unit price must be a non-negative value");
        }

        UnitPrice = newUnitPrice;
    }
}