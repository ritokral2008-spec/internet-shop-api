using InternetShop.Models;
using InternetShop.Repositories;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }
        public async Task Add(Product product)
        {
            await repository.Add(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task Remove(int id)
        {
            await repository.Remove(id);
        }
        public async Task Update(int id, Product product)
        {
            await repository.Update(id, product);   
        }
    }
}
