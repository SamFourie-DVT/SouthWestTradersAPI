using Entities.Models;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace SouthWestTradersAPI_KudaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _order;

        public OrderController(IOrderRepository order)
        {
            _order = order;
        }

       [HttpPost("Add")]
        public async Task<ActionResult<Order>> Add(Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest("Order cannot be null.");

                var availableStock = _order.GetAvailableStock(order.ProductId);

                if (availableStock == 0)
                {
                    return BadRequest("Error: No stock for this product");
                }

                if (availableStock < order.Quantity)
                {
                    return BadRequest("Error: Not enough stock for this order.");
                }

                var newOrder = await _order.CreateOrder(order);

                return Ok(newOrder);
            } 
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            try
            {
                var orders = await _order.GetAllOrders();

                if (orders == null)
                    return NotFound("Error - Unable to get any orders.");

                return Ok(orders);
            } 
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }

        [HttpPut("Cancel{id}")]
        public async Task<ActionResult<Order>> Cancel(int id)
        {
            try
            {
                var currentStatus = _order.GetCurrentStatus(id);

                if (currentStatus == 1)
                {
                    return BadRequest("Error - Cannot cancel a completed order.");
                }

                if (currentStatus == -1)
                {
                    return BadRequest("Error - Order has already been cancelled.");
                }

                var cancelledOrder = await _order.CancelOrder(id);
                return Ok(cancelledOrder);
            } 
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }
    }
}
