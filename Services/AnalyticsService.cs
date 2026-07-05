using InternetShop.Models;

namespace InternetShop.Services
{
    public class AnalyticsService
    {
        public void AddOrderToStatistics(Order order)
        {
            Console.WriteLine($"Заказ {order.Id} добавлен в статистику");
        }
    }
}
