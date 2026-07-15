using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<Product> GetById(int id);
        Task Remove(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Update(int id, Product product);
    }
}
