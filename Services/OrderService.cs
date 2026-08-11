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
        private readonly ILogger<OrderService> _logger;
        public event Action<Order>? OrderCreated;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ResponseOrderDto> CreateOrder(CreateOrderDto dto, int userId)
        {
            //Order
            var order = new Order()
            {
                UserId = userId
            };

            _logger.LogInformation(
                "Создание заказа {Id}",
                order.Id);

            foreach(var item in dto.Items)
            {
                var product = await _productRepository.GetById(item.ProductId);

                if(product.Stock < item.Quantity)
                {
                    _logger.LogError(
                        "Недостаточно товаров на складе для создания заказа");

                    throw new NotEnoughStockException("Недостаточно товара на складе");
                }

                    order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                order.TotalPrice =
                    order.Items.Sum(i => i.Quantity * i.UnitPrice);
            }

            await _orderRepository.Add(order);

            OrderCreated?.Invoke(order);

            _logger.LogInformation(
                "Заказ {Id} успешно создан",
                order.Id);

            return OrderMapper.ToDto(order);
        }

        public async Task<IEnumerable<ResponseOrderDto>> GetAll(OrderQueryDto query)
        {
            _logger.LogInformation(
                "Получение заказов"
                );

            var orders = await _orderRepository.GetAll(query);

            _logger.LogInformation(
                "Заказов получено: {Count}",
                orders.Count());

            return orders
                .Select(OrderMapper.ToDto)
                .ToList();
        }

        public async Task<ResponseOrderDto> GetById(int id)
        {
            _logger.LogInformation(
                "Получение заказа {Id}",
                id);

            var order = await _orderRepository.GetById(id);

            _logger.LogInformation(
                "Заказ {Id} успешно получен",
                id);

            return OrderMapper.ToDto(order);
        }
        public async Task<IEnumerable<ResponseOrderDto>> GetByUserId(int userId, OrderQueryDto query)
        {
            _logger.LogInformation(
                "Получение заказов {userId} пользователя",
                userId);

            var orders = await _orderRepository.GetByUserId(userId, query);

            _logger.LogInformation(
                "Заказы {userId} пользователя успешно получены",
                userId);

            return orders
                .Select(OrderMapper.ToDto)
                .ToList();
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

            _logger.LogInformation(
                "Обновление заказа {Id}",
                id);

            await _orderRepository.Update(order);

            _logger.LogInformation(
                "Заказ {Id} успешно обновлён",
                id);

            return OrderMapper.ToDto(order);

        }

        public async Task Remove(int id)
        {
            _logger.LogInformation(
                "Удаление заказа {Id}",
                id);

            await _orderRepository.Remove(id);

            _logger.LogInformation(
                "Заказ {Id} успешно удалён",
                id);
        }

        
    }
}
