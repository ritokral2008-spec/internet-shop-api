using InternetShop.DTOs.Orders;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderQueryDto query)
        {
            var response = await _service.GetAll(query);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders([FromQuery] OrderQueryDto query)
        {
            int userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
                );

            var response = await _service.GetByUserId(userId, query);

            return Ok(response);
        }

        [Authorize]
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

            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await _service.CreateOrder(dto, userId);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetById(id);

            return Ok(response);
        }

        [Authorize]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.Remove(id);

            return NoContent();
        }
    }
}
