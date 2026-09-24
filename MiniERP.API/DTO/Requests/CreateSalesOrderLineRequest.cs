public class CreateSalesOrderLineRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateOnly DeliveryDate { get; set; }
}