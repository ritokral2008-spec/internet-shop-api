using InternetShop.DTOs.Orders;
using InternetShop.DTOs.OrderItems;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IValidator<CreateOrderDto> _createValidator;
        private readonly IValidator<UpdateOrderDto> _updateValidator;
        public OrdersController(
            IOrderService service,
            IValidator<CreateOrderDto> createValidator,
            IValidator<UpdateOrderDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _service.GetAll();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var result = await _createValidator.ValidateAsync(dto);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage));

                return BadRequest(errors);
            }

            var response = await _service.CreateOrder(dto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetById(id);

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage));

                return BadRequest(errors);
            }

            var response = await _service.Update(id, dto);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.Remove(id);

            return NoContent();
        }
    }
}
