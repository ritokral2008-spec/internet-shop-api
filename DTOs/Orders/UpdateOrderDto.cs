using InternetShop.DTOs.OrderItems;

namespace InternetShop.DTOs.Orders
{
    public class UpdateOrderDto
    {
        public List<UpdateOrderItemDto> Items { get; set; } = new();
    }
}
