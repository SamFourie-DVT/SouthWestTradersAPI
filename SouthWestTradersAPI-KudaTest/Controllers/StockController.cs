using Entities.Models;
using Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace SouthWestTradersAPI_KudaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stock;

        public StockController(IStockRepository stock)
        {
            _stock = stock;
        }

        [HttpGet("GetAvailableStockByProductId{Id}")]
        public async Task<ActionResult<List<Product>>> GetAvailableStock(int Id)
        {
            try
            {
                var stock = await _stock.GetAvailableStockForProduct(Id);

                if (stock == null)
                    return NotFound("Stock For This Product Not Found.");

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }

       [HttpPost("Add")]
        public async Task<ActionResult<Stock>> Add(Stock stock)
        {
            try
            {
                if (stock == null)
                    return BadRequest("Stock cannot be null.");

                var newStock = await _stock.AddStock(stock);

                if (newStock == null)
                    return BadRequest("ProductId entered does not exist in the Product table.");

                return Ok(newStock);
            } 
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }
    }
}
