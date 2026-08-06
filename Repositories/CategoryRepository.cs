using InternetShop.Data;
using InternetShop.DTOs.Categories;
using InternetShop.Exceptions;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll(CategoryQueryDto query)
        {
            IQueryable<Category> categories = _context.Categories;

            if(!string.IsNullOrWhiteSpace(query.Name))
            {
                categories = categories.Where(c =>
                c.Name.Contains(query.Name));
            }

            return categories
                .Include(p => p.Products)
                .ToList();
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories
                .Include(p => p.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(category == null)
                throw new CategoryNotFoundException("Категория не найдена");
            

            return category;
        }
        public async Task<Category> Update(int id, Category category)
        {
            var existing = await _context.Categories.FindAsync(id);

            if(existing == null)
                throw new CategoryNotFoundException("Категория не найдена");

            existing.Name = category.Name;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task Remove(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if(category == null)
                return;

            _context.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
