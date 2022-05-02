﻿using Entities.Models;
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

                var checkStock = _order.CheckAvailableStock(order.ProductId, order.Quantity);

                if (checkStock == false)
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
    }
}