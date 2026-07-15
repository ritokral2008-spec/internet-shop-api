using InternetShop.DTOs.Categories;
using InternetShop.Models;

namespace InternetShop.Mappers
{
    public static class CategoryMapper
    {
        public static ResponseCategoryDto ToDto(Category category)
        {
            return new ResponseCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
