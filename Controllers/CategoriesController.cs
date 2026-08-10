using FluentValidation;
using InternetShop.DTOs.Categories;
using InternetShop.Models;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController: ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;

        public CategoriesController(
            ICategoryService service,
            IValidator<CreateCategoryDto> createValidator,
            IValidator<UpdateCategoryDto> updateValidaor)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidaor;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoryQueryDto query)
        {
            var response = await _service.GetAll(query);
            
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryDto dto)
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

            var response = await _service.Add(dto);

            return CreatedAtAction(
                nameof(Get),
                new { id = response.Id },
                response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetById(id);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
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
