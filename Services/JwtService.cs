using InternetShop.Models;
using InternetShop.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternetShop.Services
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtService> _logger;

        public JwtService(
            IConfiguration configuration,
            ILogger<JwtService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public string GenerateToken(User user)
        {
            _logger.LogInformation(
                "Создание токена"
                );

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToInt32(_configuration["Jwt:Expires"])),
                signingCredentials: credentials);

            _logger.LogInformation(
                "Токен успешно создан"
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
