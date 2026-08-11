using InternetShop.DTOs.Authentication.Login;
using InternetShop.DTOs.Authentication.Register;
using InternetShop.DTOs.Authentication.User;
using InternetShop.Mappers;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = logger;
        }
        public async Task<ResponseUserDto> Register(RegisterRequestDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Admin"
            };

            _logger.LogInformation(
                "Регистрация пользователя"
                );

            await _userRepository.Add(user);

            _logger.LogInformation(
                "Пользователь успешно зарегистрирован"
                );

            return new ResponseUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByUsername(dto.Username);

            if(user == null)
                throw new UnauthorizedAccessException(
                    "Неверный логин или пароль");

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash 
                    );

            if(!validPassword)
                throw new UnauthorizedAccessException(
                    "Неверный логин или пароль");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}
