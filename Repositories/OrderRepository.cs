using InternetShop.Data;
using InternetShop.Exceptions;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null)
                throw new OrderNotFoundException("Заказ не найден");

            return order;
        }

        public async Task Remove(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
                return;

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }
        public async Task<Order> Update(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
