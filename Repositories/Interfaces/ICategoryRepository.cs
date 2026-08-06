using InternetShop.DTOs.Categories;
using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task Add(Category category);
        Task<IEnumerable<Category>> GetAll(CategoryQueryDto query);
        Task<Category> GetById(int id);
        Task<Category> Update(int id, Category category);
        Task Remove(int id);
    }
}
