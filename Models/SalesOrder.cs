using ExcelMerger.Models;

public class SalesOrder
{
    private List<SalesOrderLine> salesOrderLines = new List<SalesOrderLine>();
    public IReadOnlyList<SalesOrderLine> SalesOrderLines => salesOrderLines;

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string OrderNumber { get; private set; }
   
    public decimal GetOrderTotalPrice()
    {
        decimal total = 0;
        foreach (var salesOrderLine in salesOrderLines)
        {
            total += salesOrderLine.RowTotalPrice;
        }

        return total;

    }

    public SalesOrder(Guid customerId, string orderNumber)
    {
        
        Id = Guid.NewGuid();
        CustomerId = customerId;
        OrderNumber = orderNumber;
    }

    public void AddLine(SalesOrderLine line)
    {
        salesOrderLines.Add(line);
    }
}
    