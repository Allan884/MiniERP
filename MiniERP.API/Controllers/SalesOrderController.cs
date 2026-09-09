using ExcelMerger.Service;

namespace MiniERP.API.Controllers;

public class SalesOrderController
{
    private readonly SalesOrderService salesOrderService;

    public SalesOrderController(SalesOrderService salesOrderService)
    {
        this.salesOrderService = salesOrderService;
    }
}