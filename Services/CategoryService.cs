using InternetShop.DTOs.Categories;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;
using InternetShop.Mappers;

namespace InternetShop.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            ICategoryRepository repository,
            ILogger<CategoryService> logger
            )
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<ResponseCategoryDto> Add(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _logger.LogInformation(
                "Создание категории {Name}",
                dto.Name);

            await _repository.Add(category);

            _logger.LogInformation(
                "Категория {Name} успешно создана с Id = {Id}",
                category.Name,
                category.Id);

            return CategoryMapper.ToDto(category);
        }

        public async Task<IEnumerable<ResponseCategoryDto>> GetAll(CategoryQueryDto query)
        {
            var categories = await _repository.GetAll(query);

            _logger.LogInformation(
                "Получено {Count} категорий",
                categories.Count());

            return categories
                .Select(CategoryMapper.ToDto)
                .ToList();
        }

        public async Task<ResponseCategoryDto> GetById(int id)
        {
            _logger.LogInformation(
                "Получение категории с Id = {Id}",
                id);

            var category = await _repository.GetById(id);

            _logger.LogInformation(
                "Категория с Id = {Id} успешно найдена",
                id);

            return CategoryMapper.ToDto(category);
        }

        public async Task<ResponseCategoryDto> Update(int id, UpdateCategoryDto dto)
        {
            var category = new Category
            {
                Id = id,
                Name = dto.Name
            };

            _logger.LogInformation(
                "Обновление категории {Id}, Новое имя: {Name}",
                category.Id,
                category.Name);

            var updated = await _repository.Update(id, category);

            _logger.LogInformation(
                "Категория {Id} успешно обновлена",
                category.Id);

            return CategoryMapper.ToDto(updated);
        }
        public async Task Remove(int id)
        {
            await _repository.Remove(id);

            _logger.LogInformation(
                "Удаление категории {Id}",
                id);
        }
    }
}
