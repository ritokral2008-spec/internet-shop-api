using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IWarehouseService
    {
        public Task UpdateRepository(Order order);
    }
}
