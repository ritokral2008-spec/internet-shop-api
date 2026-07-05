using InternetShop.Models;
using InternetShop.Repositories;
using InternetShop.Exceptions;

namespace InternetShop.Services
{
    public class OrderService: IOrderService<Order>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly WarehouseService warehouseService;
        private readonly PaymentService paymentService;
        public event Action<Order>? OrderCreated;

        public OrderService(
            IRepository<Order> orderRepository,
            WarehouseService warehouseService,
            PaymentService paymentService)
        {
            this.orderRepository = orderRepository;
            this.warehouseService = warehouseService;
            this.paymentService = paymentService;
        }

        public async Task CreateOrder(Order order)
        {
            await paymentService.PayAsync(order);

            warehouseService.UpdateRepository(order);

            orderRepository.Add(order);

            OrderCreated?.Invoke(order);
        }

        public Task<IEnumerable<Order?>> GetAll()
        {
            return orderRepository.GetAll();
        }

        public Task<Order> GetById(int id)
        {
            return orderRepository.GetById(id);
        }

        public void Remove(int id)
        {
            orderRepository.Remove(id);
        }
    }
}
