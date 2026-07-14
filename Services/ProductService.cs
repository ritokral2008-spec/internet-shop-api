using InternetShop.DTOs.Products;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;

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
            var category = await _categoryRepository.GetById(dto.CategoryId);

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            await _productRepository.Add(product);

            var response = new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName
            };

            return response;
        }

        public async Task<IEnumerable<ResponseProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();

            var response = new List<ResponseProductDto>();

            foreach(var product in products)
            {
                response.Add(new ResponseProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    CategoryName = product.CategoryName
                });
            }

            return response;
        }

        public async Task<ResponseProductDto> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            var category = await _categoryRepository.GetById(product.CategoryId);

            var response = new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName
            };
            return response;
        }

        public async Task Remove(int id)
        {
            await _productRepository.Remove(id);
        }
        public async Task<ResponseProductDto> Update(int id, UpdateProductDto dto)
        {
            var category = await _categoryRepository.GetById(dto.CategoryId);

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };
            await _productRepository.Update(id, product);   

            var response = new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName
            };

            return response;
        }
    }
}
