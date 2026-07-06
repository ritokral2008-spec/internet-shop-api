using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
                throw new ProductNotFoundException("Товар не найден");

            return product;
        }

        public async Task Remove(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
                throw new ProductNotFoundException("Товар не найден");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
        public async Task Update(int id, Product product)
        {
            var existing = await _context.Products.FindAsync(id);

            if(existing == null)
                throw new ProductNotFoundException("Товар не найден");

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;

            await _context.SaveChangesAsync();
        }
    }
}
