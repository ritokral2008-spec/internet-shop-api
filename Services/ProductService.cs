using InternetShop.Models;
using InternetShop.Repositories;

namespace InternetShop.Services
{
    public class ProductService: IProductService<Product>
    {
        private readonly IProductRepository<Product> repository;

        public ProductService(IProductRepository<Product> repository)
        {
            this.repository = repository;
        }
        public void Add(Product product)
        {
            repository.Add(product);
        }

        public async Task<IEnumerable<Product?>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public void Remove(int id)
        {
            repository.Remove(id);
        }
        public async Task Update(int id, Product product)
        {
            repository.Update(id, product);   
        }
    }
}
