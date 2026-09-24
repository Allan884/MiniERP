using ExcelMerger.Service;
using ExcelMerger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using ExcelMerger.Exceptions;

namespace MiniERP.API.Controllers;

[Route("api/salesorders")] // Define the route for the controller
public class SalesOrderController : ControllerBase // Inherit from ControllerBase for API controllers
{
    private readonly SalesOrderService salesOrderService;
    private readonly ProductService productService;

    public SalesOrderController(SalesOrderService salesOrderService, ProductService productService) 
    {
        this.salesOrderService = salesOrderService;
        this.productService = productService;
    }


    [HttpGet] // GET api/salesorders
    public IReadOnlyList<SalesOrder> GetSalesOrders()
    {
        return salesOrderService.GetSalesOrders();
    }

    [HttpGet("{id}")] // GET api/salesorders/id
    public ActionResult<SalesOrder> GetSalesOrderById(Guid id)
    {
        var salesOrder = salesOrderService.GetOrderById(id);
        if (salesOrder == null)
        {
            return NotFound();
        }
        
        return Ok(salesOrder);
    }

    [HttpPost] // POST api/salesorders
    public IActionResult CreateSalesOrder([FromBody] CreateSalesOrderRequest request)
    {
        var customerId = request.CustomerId;
        var salesOrder = salesOrderService.CreateSalesOrder(customerId);

        var response = new SalesOrderResponse
        {
            CustomerId = salesOrder.CustomerId,
            OrderNumber = salesOrder.OrderNumber
        };

        return Ok(response);
    }

    [HttpPost("{orderNumber}/lines")]
    public IActionResult AddSalesOrderLine(
        string orderNumber,
        [FromBody] CreateSalesOrderLineRequest request)
    {
        Console.WriteLine($"Order number: {orderNumber}");
        
        var productId = request.ProductId;
        var quantity = request.Quantity;
        var deliveryDate = request.DeliveryDate;


        try
        {
           salesOrderService.AddLineToSalesOrder(orderNumber, productId, quantity, deliveryDate); 
 
        }
        catch (ProductNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }
}