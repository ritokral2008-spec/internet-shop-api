using InternetShop.Models;
using InternetShop.Repositories;
using InternetShop.Exceptions;
using InternetShop.Services.Interfaces;
using InternetShop.DTOs.Orders;

namespace InternetShop.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseService _warehouseService;
        private readonly IPaymentService _paymentService;
        public event Action<Order>? OrderCreated;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IWarehouseService warehouseService,
            IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _warehouseService = warehouseService;
            _paymentService = paymentService;
        }

        public async Task CreateOrder(CreateOrderDto dto)
        {
            var order = new Order();

            foreach(var item in dto.Items)
            {
                var product = await _productRepository.GetById(item.ProductId);

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }
            order.TotalPrice = order.Items.Sum(i => i.UnitPrice * i.Quantity);

            await _orderRepository.Add(order);

            OrderCreated?.Invoke(order);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }
        public async Task Update(int id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetById(id);

            order.Items.Clear();

            decimal totalPrice = 0;

            foreach(var item in dto.Items)
            {
                var product = await _productRepository.GetById(item.ProductId);

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Product = product,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                order.Items.Add(orderItem);

                totalPrice += product.Price * item.Quantity;
            }
            order.TotalPrice = totalPrice;

            await _orderRepository.Update(order);
        }

        public async Task Remove(int id)
        {
            await _orderRepository.Remove(id);
        }
    }
}
