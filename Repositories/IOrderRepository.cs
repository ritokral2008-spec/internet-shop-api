using InternetShop.Models;

namespace InternetShop.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<Order> GetById(int id);
        Task Remove(int id);
        Task<IEnumerable<Order>> GetAll();
    }
}
