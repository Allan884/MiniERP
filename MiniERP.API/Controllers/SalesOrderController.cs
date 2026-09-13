using ExcelMerger.Service;
using ExcelMerger.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiniERP.API.Controllers;

[Route("api/salesorders")] // Define the route for the controller
public class SalesOrderController : ControllerBase // Inherit from ControllerBase for API controllers
{
    private readonly SalesOrderService salesOrderService;

    public SalesOrderController(SalesOrderService salesOrderService) 
    {
        this.salesOrderService = salesOrderService;
    }


    [HttpGet] // GET api/salesorders
    public IReadOnlyList<SalesOrder> GetSalesOrders()
    {
        return salesOrderService.GetSalesOrders();
    }
}