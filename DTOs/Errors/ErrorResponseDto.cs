namespace InternetShop.DTOs.Errors
{
    public class ErrorResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; } = "";
        public string Path { get; set; } = "";
        public DateTime TimeStamp { get; set; }
    }
}
