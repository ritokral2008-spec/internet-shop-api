using InternetShop.DTOs.Orders;
using InternetShop.DTOs.OrderItems;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(
            IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _service.GetAll();

            var response = orders.Select(order => new ResponseOrderDto
            {
                Id = order.Id,
                Status = order.Status,
                TotalPrice = order.TotalPrice,

                Items = order.Items.Select(item => new ResponseOrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                }).ToList()
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            await _service.CreateOrder(dto);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _service.GetById(id);

            return Ok(order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
        {
            await _service.Update(id, dto);
            return Ok(dto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return NoContent();
        }
    }
}
