using InternetShop.Data;
using InternetShop.DTOs;
using InternetShop.DTOs.Products;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductService _service;

        public ProductsController(
            ICategoryRepository categoryRepository,
            IProductService service)
        {
            _categoryRepository = categoryRepository;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
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
