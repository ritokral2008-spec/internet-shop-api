using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Services.Interfaces;
using InternetShop.DTOs.Orders;
using InternetShop.Repositories.Interfaces;
using InternetShop.DTOs.OrderItems;

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

            foreach(var item in order.Items)
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
            var response = new ResponseOrderDto();

            foreach(var item in dto.Items)
            {
                var product = await _productRepository.GetById(item.ProductId);

                response.Items.Add(new ResponseOrderItemDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }
            response.TotalPrice = response.Items.Sum(i => i.UnitPrice * i.Quantity);

            await _orderRepository.Add(order);

            OrderCreated?.Invoke(order);

            return response;
        }

        public async Task<IEnumerable<ResponseOrderDto>> GetAll()
        {
            var orders = await _orderRepository.GetAll();

            var response = new ResponseOrderDto();

            var responses = new List<ResponseOrderDto>();

            foreach(var item in orders)
            {
                response.Id = item.Id;
                response.TotalPrice = item.TotalPrice;
                response.Status = item.Status;

                foreach(var orderItem in item.Items) 
                {
                    response.Items.Add(new ResponseOrderItemDto
                    {
                        ProductId = orderItem.ProductId,
                        ProductName = orderItem.ProductName,
                        UnitPrice = orderItem.UnitPrice,
                        Quantity = orderItem.Quantity
                    });
                }
                responses.Add(response);
            }

            return responses;
        }

        public async Task<ResponseOrderDto> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);

            var response = new ResponseOrderDto();

            response.Status = order.Status;
            response.Id = order.Id;
            response.TotalPrice = order.TotalPrice;

            foreach(var item in order.Items)
            {
                response.Items.Add(new ResponseOrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                });
            }

            return response;
        }
        public async Task<ResponseOrderDto> Update(int id, UpdateOrderDto dto)
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

            //Response
            var response = new ResponseOrderDto();

            response.Id = order.Id;
            response.Status = order.Status;
            response.TotalPrice = order.TotalPrice;

            foreach(var item in order.Items)
            {
                response.Items.Add(new ResponseOrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                });
            }

            return response;

        }

        public async Task Remove(int id)
        {
            await _orderRepository.Remove(id);
        }
    }
}
