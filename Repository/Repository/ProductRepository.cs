using Entities.Models;
using Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SouthWestTradersContext _data;

        public ProductRepository(SouthWestTradersContext data)
        {
            _data = data;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _data.Products.Add(product);
            await _data.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product product = await _data.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            _data.Products.Remove(product);
            await _data.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _data.Products.ToListAsync();
            return products;
        }

        public IQueryable<Product> SearchProductByName(string searchName)
        {
            var productSearchResults = _data.Products.Where(n => n.Name.Contains(searchName));
            return productSearchResults;
        }
    }
}
