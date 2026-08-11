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
        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
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

            await _userRepository.Add(user);

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
        public async Task<IEnumerable<ResponseUserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();

            return users
                .Select(UserMapper.ToDto)
                .ToList();
        }

        public async Task<ResponseUserDto> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            return UserMapper.ToDto(user);
        }
        public async Task Remove(int id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<ResponseUserDto> Update(int id, UpdateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = dto.Password
            };

            var updated = await _userRepository.Update(id, user);

            return UserMapper.ToDto(updated);
        }
    }
}
