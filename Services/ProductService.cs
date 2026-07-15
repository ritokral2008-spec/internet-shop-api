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

        public ProductService(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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

            await _productRepository.Add(product);

            return ProductMapper.ToDto(product);
        }

        public async Task<IEnumerable<ResponseProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();

            return products
                .Select(ProductMapper.ToDto)
                .ToList();
        }

        public async Task<ResponseProductDto> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            return ProductMapper.ToDto(product);
        }

        public async Task Remove(int id)
        {
            await _productRepository.Remove(id);
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
            var updated = await _productRepository.Update(id, product);

            return ProductMapper.ToDto(product);
        }
    }
}
