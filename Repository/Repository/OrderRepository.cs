using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SouthWestTradersContext _data;

        public OrderRepository(SouthWestTradersContext data)
        {
            _data = data;
        }

        public Task<Order> CancelOrder(int Id)
        {
            throw new NotImplementedException();
        }

        public bool CheckAvailableStock(int productId, int amountOrdered)
        {
            var currentStock = _data.Stocks.FirstOrDefault(x => x.ProductId == productId);

            if (amountOrdered > currentStock.AvailableStock)
            {
                return false;
            }

            return true;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            _data.Orders.Add(order);
            await _data.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _data.Orders.ToListAsync();
            return orders;
        }

        public Task<Order> SearchOrderByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<Order> SearchOrderByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
