using InternetShop.DTOs.Products;
using InternetShop.Models;

namespace InternetShop.Mappers
{
    public static class ProductMapper
    {
        public static ResponseProductDto ToDto(Product product)
        {
            return new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
        }
    }
}
