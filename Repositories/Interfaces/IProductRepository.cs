using InternetShop.DTOs.Products;
using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<Product> GetById(int id);
        Task Remove(int id);
        Task<IEnumerable<Product>> GetAll(ProductQueryDto query);
        Task<Product> Update(int id, Product product);
    }
}
