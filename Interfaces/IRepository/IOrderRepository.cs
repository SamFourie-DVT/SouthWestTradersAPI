using Entities.Models;

namespace Interfaces.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);

        Task<Order> CancelOrder(int Id);

        Task<Order> SearchOrderByDate(DateTime dateTime);

        Task<Order> SearchOrderByName(string name);

        Task<List<Order>> GetAllOrders();

        int? GetAvailableStock(int productId);

        int GetCurrentStatus(int id);


    }
}
