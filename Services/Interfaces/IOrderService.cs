using InternetShop.DTOs.Orders;
using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto dto);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task Remove(int id);
        Task Update(int id, UpdateOrderDto dto);
    }
}
