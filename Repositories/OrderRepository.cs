using InternetShop.Data;
using InternetShop.Models;
using System.ComponentModel.Design.Serialization;

namespace InternetShop.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return _context.Orders.ToList();
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
                throw new Exception("Заказ не найден");

            return order;
        }

        public async Task Remove(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
                return;

            _context.Orders.Remove(order);
        }
    }
}
