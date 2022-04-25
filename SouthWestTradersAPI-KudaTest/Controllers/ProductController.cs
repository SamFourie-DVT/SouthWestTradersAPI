using Entities.Models;
using Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace SouthWestTradersAPI_KudaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _products;
        private readonly SouthWestTradersContext _data;

        public ProductController(SouthWestTradersContext data, IProductRepository products)
        {
            _data = data;
            _products = products;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _products.GetAllProducts();

                if (products == null)
                    return BadRequest("Error. Trying fetching all the products but was unable to do so.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }

        [HttpGet("SearchByName{searchName}")]
        public async Task<ActionResult<List<Product>>> SearchProductByName(string searchName)
        {
            try
            {
                IQueryable<Product> products = _products.SearchProductByName(searchName);

                if (products.Count() == 0)
                    return NotFound("No Products Found.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (product == null)
                return BadRequest("Please enter product details.");

            try
            {
                await _products.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }
        }

        [HttpDelete("Delete{Id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int Id)
        {
            var product = _data.Products.FirstOrDefault(x => x.ProductId == Id);

            if (product == null)
                return NotFound("Product Not Found.");

            try
            {
                await _products.DeleteProduct(Id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("An Unexpected Error Has Occured. Error Message --> " + ex);
            }

            
        }
    }
}
