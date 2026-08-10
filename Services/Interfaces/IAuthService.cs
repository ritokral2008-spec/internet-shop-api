using InternetShop.DTOs.Authentication.Login;
using InternetShop.DTOs.Authentication.Register;
using InternetShop.DTOs.Authentication.User;

namespace InternetShop.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseUserDto> Register(RegisterRequestDto dto);
        Task<LoginResponseDto> Login(LoginRequestDto dto);
    }
}
