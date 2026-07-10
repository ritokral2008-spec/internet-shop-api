using InternetShop.DTOs.OrderItems;

namespace InternetShop.DTOs.Orders
{
    public class ResponseOrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "";
        public List<ResponseOrderItemDto> Items { get; set; } = new();
    }
}
