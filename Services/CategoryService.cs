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

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResponseCategoryDto> Add(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };
            await _repository.Add(category);

            return CategoryMapper.ToDto(category);
        }

        public async Task<IEnumerable<ResponseCategoryDto>> GetAll()
        {
            var categories = await _repository.GetAll();

            var response = new List<ResponseCategoryDto>();
            foreach(var category in categories)
            {
                response.Add(CategoryMapper.ToDto(category));
            }

            return response;
        }

        public async Task<ResponseCategoryDto> GetById(int id)
        {
            var category = await _repository.GetById(id);

            return CategoryMapper.ToDto(category);
        }

        public async Task<ResponseCategoryDto> Update(int id, UpdateCategoryDto dto)
        {
            var category = new Category
            {
                Id = id,
                Name = dto.Name
            };
            await _repository.Update(id, category);

            return CategoryMapper.ToDto(category);
        }
        public async Task Remove(int id)
        {
            await _repository.Remove(id);
        }
    }
}
