using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task Add(Category category);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Update(int id, Category category);
        Task Remove(int id);
    }
}
