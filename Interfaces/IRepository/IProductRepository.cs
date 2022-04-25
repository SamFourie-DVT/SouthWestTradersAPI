using Entities.Models;

namespace Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        IQueryable<Product> SearchProductByName(string searchName);

        Task<Product> AddProduct(Product product);

        Task<Product> DeleteProduct(int id);
    }
}
