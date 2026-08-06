using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Services.Interfaces;
using InternetShop.Data;
using InternetShop.Repositories.Interfaces;

namespace InternetShop.Services
{
    public class WarehouseService: IWarehouseService
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;
        public WarehouseService(
            AppDbContext context,
            IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        public async Task UpdateRepository(Order order)
        {
            foreach(var orderProduct in order.Items)
            {
                var product = await _productRepository.GetById(orderProduct.ProductId);

                if(product == null)
                    throw new ProductNotFoundException("Товар не найден");

                if(product.Stock < orderProduct.Quantity)
                    throw new NotEnoughStockException("Недостаточно товара на складе");

                product.Stock -= orderProduct.Quantity;

                await _context.SaveChangesAsync();
            }
        }
    }
}
