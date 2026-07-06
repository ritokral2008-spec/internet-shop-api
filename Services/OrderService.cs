using InternetShop.Models;
using InternetShop.Repositories;
using InternetShop.Exceptions;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IWarehouseService warehouseService;
        private readonly IPaymentService paymentService;
        public event Action<Order>? OrderCreated;

        public OrderService(
            IOrderRepository orderRepository,
            IWarehouseService warehouseService,
            IPaymentService paymentService)
        {
            this.orderRepository = orderRepository;
            this.warehouseService = warehouseService;
            this.paymentService = paymentService;
        }

        public async Task CreateOrder(Order order)
        {
            await paymentService.PayAsync(order);

            warehouseService.UpdateRepository(order);

            await orderRepository.Add(order);

            OrderCreated?.Invoke(order);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await orderRepository.GetAll();
        }

        public async Task<Order> GetById(int id)
        {
            return await orderRepository.GetById(id);
        }

        public void Remove(int id)
        {
            orderRepository.Remove(id);
        }
    }
}
