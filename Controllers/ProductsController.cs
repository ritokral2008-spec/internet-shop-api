using InternetShop.Data;
using InternetShop.DTOs;
using InternetShop.DTOs.Products;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductService _service;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductsController(
            ICategoryRepository categoryRepository,
            IProductService service,
            IValidator<CreateProductDto> createValidator,
            IValidator<UpdateProductDto> updateValidator)
        {
            _categoryRepository = categoryRepository;
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryDto query)
        {
            var response = await _service.GetAll(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto dto)
        {
            var result = await _createValidator.ValidateAsync(dto);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

                return BadRequest(errors);
            }
               

            var category = await _categoryRepository.GetById(dto.CategoryId);

            var response = await _service.Add(dto);

            return CreatedAtAction(
                nameof(Get),
                new { id = response.Id },
                response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

                return BadRequest(errors);
            }

            var response = await _service.Update(id, dto);

            return Ok(
                response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Remove(id);

            return NoContent();
        }
    }
}
