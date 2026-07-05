using InternetShop.Models;

namespace InternetShop.Services
{
    public class EmailService
    {
        public void SendEmail(Order order)
        {
            Console.WriteLine($"Email отправлен владельцу {order.Id}");
        }
    }
}
