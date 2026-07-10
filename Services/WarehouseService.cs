using InternetShop.Repositories;
using InternetShop.Models;
using InternetShop.Exceptions;
using InternetShop.Services.Interfaces;
using InternetShop.Data;

namespace InternetShop.Services
{
    public class WarehouseService: IWarehouseService
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository productRepository;
        public WarehouseService(
            AppDbContext context,
            IProductRepository productRepository)
        {
            _context = context;
            this.productRepository = productRepository;
        }
        public async Task UpdateRepository(Order order)
        {
            foreach(var orderProduct in order.Items)
            {
                var product = await productRepository.GetById(orderProduct.ProductId);

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
