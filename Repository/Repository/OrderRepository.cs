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

        public async Task<Order> CancelOrder(int Id)
        {
            var order = _data.Orders.FirstOrDefault(x => x.ProductId == Id);

            if (order == null)
            {
                return null;
            }

            order.OrderStateId = -1;
            await _data.SaveChangesAsync();

            return order;
        }

        public int? GetAvailableStock(int productId)
        {
            var currentStock = _data.Stocks.FirstOrDefault(x => x.ProductId == productId);
            return currentStock.AvailableStock;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var stock = _data.Stocks.FirstOrDefault(x => x.ProductId == order.ProductId);

            _data.Orders.Add(order);
            stock.AvailableStock = stock.AvailableStock - order.Quantity;
            _data.Stocks.Update(stock);
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

        public int GetCurrentStatus(int id)
        {
            var currentStatus = _data.Orders.FirstOrDefault(x => x.OrderId == id);
            return currentStatus.OrderStateId;
        }
    }
}
