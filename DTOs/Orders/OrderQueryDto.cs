namespace InternetShop.DTOs.Orders
{
    public class OrderQueryDto
    {
        public string? Status { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public decimal? MaxTotalPrice { get; set; }
    }
}
