using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;
using InternetShop.Repositories.Interfaces;
using InternetShop.DTOs.Products;

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

        public async Task<IEnumerable<Product>> GetAll(ProductQueryDto query)
        {
            IQueryable<Product> products = _context.Products;

            if(!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p =>
                p.Name.Contains(query.Name));
            }

            if(query.CategoryId.HasValue)
            {
                products = products.Where(p =>
                p.CategoryId == query.CategoryId);
            }

            if(query.MinPrice.HasValue)
            {
                products = products.Where(p =>
                p.Price >= query.MinPrice);
            }

            if(query.MaxPrice.HasValue)
            {
                products = products.Where(p =>
                p.Price <= query.MaxPrice);
            }

            switch(query.SortBy?.ToLower())
            {
                case "price":

                    products = query.Descending
                        ? products.OrderByDescending(x => x.Price)
                        : products.OrderBy(x => x.Price);

                break;

                case "name":

                    products = query.Descending
                        ? products.OrderByDescending(x => x.Name)
                        : products.OrderBy(x => x.Name);

                break;

                default:

                    products = products.OrderBy(x => x.Id);

                    break;
            }

            products = products
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);

            return await products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

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
        public async Task<Product> Update(int id, Product product)
        {
            var existing = await _context.Products.FindAsync(id);

            if(existing == null)
                throw new ProductNotFoundException("Товар не найден");

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            existing.CategoryId = product.CategoryId;
            existing.CategoryName = product.CategoryName;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
