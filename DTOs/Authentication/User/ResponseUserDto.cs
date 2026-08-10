namespace InternetShop.DTOs.Authentication.User
{
    public class ResponseUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
