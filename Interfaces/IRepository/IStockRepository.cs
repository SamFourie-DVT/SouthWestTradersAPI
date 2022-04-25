using Entities.Models;

namespace Interfaces.Repository
{
    public interface IStockRepository
    {
        Task<Stock> GetAvailableStockForProduct(int Id);

        Task<Stock> AddStock(Stock stock);
    }
}
