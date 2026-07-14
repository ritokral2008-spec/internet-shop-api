using InternetShop.DTOs.Orders;
using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseOrderDto> CreateOrder(CreateOrderDto dto);
        Task<IEnumerable<ResponseOrderDto>> GetAll();
        Task<ResponseOrderDto> GetById(int id);
        Task Remove(int id);
        Task<ResponseOrderDto> Update(int id, UpdateOrderDto dto);
    }
}
