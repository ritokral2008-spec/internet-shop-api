using InternetShop.Models;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class PaymentService: IPaymentService
    {
        public async Task<bool> PayAsync(Order order)
        {
            await Task.Delay(1000);

            Console.WriteLine("Оплата прошла успешно");

            return true;
        }
    }
}
