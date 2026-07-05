using InternetShop.Models;

namespace InternetShop.Services
{
    public class PaymentService
    {
        public async Task<bool> PayAsync(Order order)
        {
            await Task.Delay(1000);

            Console.WriteLine("Оплата прошла успешно");

            return true;
        }
    }
}
