using InternetShop.DTOs.Orders;
using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<Order> GetById(int id);
        Task<IEnumerable<Order>> GetByUserId(int userId, OrderQueryDto query);
        Task Remove(int id);
        Task<IEnumerable<Order>> GetAll(OrderQueryDto query);
        Task<Order> Update(Order order);
    }
}
