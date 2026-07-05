using InternetShop.Models;
using System.ComponentModel.Design.Serialization;

namespace InternetShop.Repositories
{
    public class OrderRepository: IRepository<Order>
    {
        private readonly List<Order> orders = new();
        public void Add(Order order)
        {
            orders.Add(order);
        }

        public async Task<IEnumerable<Order?>> GetAll()
        {
            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = orders.FirstOrDefault(x => x.Id == id);

            if(order == null)
                throw new Exception("Заказ не найден");

            return order;
        }

        public void Remove(int id)
        {
            var order = orders.FirstOrDefault(x => x.Id == id);

            if(order == null)
                return;

            orders.Remove(order);
        }
    }
}
