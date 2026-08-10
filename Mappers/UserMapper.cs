using InternetShop.DTOs.Authentication.User;
using InternetShop.Models;

namespace InternetShop.Mappers
{
    public class UserMapper
    {
        public static ResponseUserDto ToDto(User user)
        {
            return new ResponseUserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}
