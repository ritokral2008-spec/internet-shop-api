using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<Order> GetById(int id);
        Task Remove(int id);
        Task<IEnumerable<Order>> GetAll();
        Task Update(Order order);
    }
}
