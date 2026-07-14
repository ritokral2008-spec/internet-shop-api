using InternetShop.DTOs.Categories;
using InternetShop.Models;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryDto dto)
        {
            var response = await _service.Add(dto);

            return CreatedAtAction(
                nameof(Get),
                new { id = response.Id },
                response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetById(id);

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
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
