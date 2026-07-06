using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        void Remove(int id);
    }
}
