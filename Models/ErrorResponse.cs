namespace InternetShop.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = "";
        public DateTime TimeStamp { get; set; }
    }
}
