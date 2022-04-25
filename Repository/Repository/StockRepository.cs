using Entities.Models;
using Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly SouthWestTradersContext _data;

        public StockRepository(SouthWestTradersContext data)
        {
            _data = data;
        }

        public async Task<Stock> AddStock(Stock stock)
        {
            var product = _data.Products.FirstOrDefault(p => p.ProductId == stock.ProductId);

            if (product == null)
                return null;

            _data.Stocks.Add(stock);
            await _data.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock> GetAvailableStockForProduct(int Id)
        {
            Stock stock = await _data.Stocks.FirstOrDefaultAsync(x => x.ProductId == Id);

            if (stock == null)
                return null;

            return stock;
        }
    }
}
