using InternetShop.Models;

namespace InternetShop.Repositories
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<Product> GetById(int id);
        Task Remove(int id);
        Task<IEnumerable<Product>> GetAll();
        Task Update(int id, Product product);
    }
}
