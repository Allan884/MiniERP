public class SalesOrderLineResponse
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal RowTotalPrice { get; set; }
    public DateOnly DeliveryDate { get; set; }
   
}