using InternetShop.DTOs.Orders;
using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseOrderDto> CreateOrder(CreateOrderDto dto, int userId);
        Task<IEnumerable<ResponseOrderDto>> GetAll(OrderQueryDto query);
        Task<ResponseOrderDto> GetById(int id);
        Task<IEnumerable<ResponseOrderDto>> GetByUserId(int userId);
        Task Remove(int id);
        Task<ResponseOrderDto> Update(int id, UpdateOrderDto dto);
    }
}
