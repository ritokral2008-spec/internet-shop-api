using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Services.Interfaces;
using InternetShop.DTOs.Orders;
using InternetShop.Repositories.Interfaces;
using InternetShop.DTOs.OrderItems;
using InternetShop.Mappers;

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

        public async Task<ResponseOrderDto> CreateOrder(CreateOrderDto dto)
        {
            //Order
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

            //Response

            await _orderRepository.Add(order);

            OrderCreated?.Invoke(order);

            return OrderMapper.ToDto(order);
        }

        public async Task<IEnumerable<ResponseOrderDto>> GetAll()
        {
            var orders = await _orderRepository.GetAll();

            return orders
                .Select(OrderMapper.ToDto)
                .ToList();
        }

        public async Task<ResponseOrderDto> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);

            return OrderMapper.ToDto(order);
        }
        public async Task<ResponseOrderDto> Update(int id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetById(id);

            order.Items.Clear();

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

            }
            order.TotalPrice =
                order.Items.Sum(i => i.UnitPrice * i.Quantity);

            await _orderRepository.Update(order);

            //Response

            return OrderMapper.ToDto(order);

        }

        public async Task Remove(int id)
        {
            await _orderRepository.Remove(id);
        }
    }
}
