using InternetShop.DTOs.OrderItems;

namespace InternetShop.DTOs.Orders
{
    public class CreateOrderDto
    {
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
