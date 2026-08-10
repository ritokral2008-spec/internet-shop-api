using InternetShop.DTOs.Authentication.Login;
using InternetShop.DTOs.Authentication.Register;
using InternetShop.DTOs.Authentication.User;
using InternetShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IAuthService _service;
        public UsersController(IAuthService service)
        {
            _service = service;
        }

        /*[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = _service.GetAll();

            return Ok(response);
        }*/

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var response = await _service.Register(dto);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var response = await _service.Login(dto);

            return Ok(response);
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = _service.GetById(id);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            var user = await _service.Update(id, dto);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.Remove(id);

            return NoContent();
        }*/
    }
}
