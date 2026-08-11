using InternetShop.DTOs.Products;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;
using InternetShop.Mappers;
using Microsoft.AspNetCore.Http.Features;

namespace InternetShop.Services
{
    public class ProductService: IProductService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ILogger<ProductService> logger)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<ResponseProductDto> Add(CreateProductDto dto)
        {
            await _categoryRepository.GetById(dto.CategoryId);

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            _logger.LogInformation(
                "Создание товара {Name}",
                dto.Name);

            await _productRepository.Add(product);

            _logger.LogInformation(
                "Товар {Name} успешно создан",
                product.Name);

            return ProductMapper.ToDto(product);
        }

        public async Task<IEnumerable<ResponseProductDto>> GetAll(ProductQueryDto query)
        {
            _logger.LogInformation(
                "Получение товаров"
                );

            var products = await _productRepository.GetAll(query);

            _logger.LogInformation(
                "Получено {Count} товаров",
                products.Count());

            return products
                .Select(ProductMapper.ToDto)
                .ToList();
        }

        public async Task<ResponseProductDto> GetById(int id)
        {
            _logger.LogInformation(
                "Получение товара {Id}",
                id);

            var product = await _productRepository.GetById(id);

            _logger.LogInformation(
                "Товар {Id} успешно получен",
                id);

            return ProductMapper.ToDto(product);
        }

        public async Task Remove(int id)
        {
            await _productRepository.Remove(id);

            _logger.LogInformation(
                "Удаление товара {Id}",
                id);
        }
        public async Task<ResponseProductDto> Update(int id, UpdateProductDto dto)
        {
            await _categoryRepository.GetById(dto.CategoryId);

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            _logger.LogInformation(
                "Обновление товара {Id}",
                id);

            var updated = await _productRepository.Update(id, product);

            _logger.LogInformation(
                "Товар {Id} успешно обновлён, новое имя: {Name}",
                id,
                updated.Name);

            return ProductMapper.ToDto(product);
        }
    }
}
