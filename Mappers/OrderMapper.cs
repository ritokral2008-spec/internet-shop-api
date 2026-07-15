using InternetShop.DTOs.OrderItems;
using InternetShop.DTOs.Orders;
using InternetShop.Models;

namespace InternetShop.Mappers
{
    public static class OrderMapper
    {
        public static ResponseOrderDto ToDto(Order order)
        {
            return new ResponseOrderDto
            {
                Id = order.Id,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                Items = order.Items
                    .Select(i => new ResponseOrderItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        UnitPrice = i.UnitPrice,
                        Quantity = i.Quantity
                    })
                    .ToList()
            };
        }
    }
}
