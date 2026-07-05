using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Services;

namespace InternetShop.Repositories
{
    public class ProductRepository: IProductRepository<Product>
    {
        private readonly List<Product> products = new();
        int id = 1;

        public void Add(Product product)
        {
            product.Id = id;
            products.Add(product);
            id++;
        }

        public async Task<IEnumerable<Product?>> GetAll()
        {
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if(product == null)
                throw new ProductNotFoundException("Товар не найден");

            return product;
        }

        public void Remove(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if(product == null)
                throw new ProductNotFoundException("Товар не найден");

            products.Remove(product);
        }
        public void Update(int id, Product product)
        {
            var existing = products.FirstOrDefault(x => x.Id == id);

            if(existing == null)
                throw new ProductNotFoundException("Товар не найден");

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;

        }
    }
}
