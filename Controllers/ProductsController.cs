using InternetShop.Data;
using InternetShop.DTOs;
using InternetShop.DTOs.Products;
using InternetShop.Models;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(
            IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await _service.Add(product);

            var response = new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

            return CreatedAtAction(
                nameof(Get),
                new { id = product.Id },
                response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await _service.Update(id, product);

            var response = new ResponseProductDto
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

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
