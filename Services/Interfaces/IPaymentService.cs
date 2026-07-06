using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> PayAsync(Order order);
    }
}
