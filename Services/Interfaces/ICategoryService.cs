using InternetShop.DTOs.Categories;
using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseCategoryDto> Add(CreateCategoryDto dto);
        Task<IEnumerable<ResponseCategoryDto>> GetAll();
        Task<ResponseCategoryDto> GetById(int id);
        Task<ResponseCategoryDto> Update(int id, UpdateCategoryDto dto);
        Task Remove(int id);
    }
}
