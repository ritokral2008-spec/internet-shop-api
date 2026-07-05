using InternetShop.Repositories;
using InternetShop.Models;
using InternetShop.Exceptions;

namespace InternetShop.Services
{
    public class WarehouseService
    {
        private readonly IRepository<Product> productRepository;
        public WarehouseService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }
        public async void UpdateRepository(Order order)
        {
            foreach(var orderProduct in order.Products)
            {
                var product = await productRepository.GetById(orderProduct.ProductId);

                if(product == null)
                    throw new ProductNotFoundException("Товар не найден");

                if(product.Stock < orderProduct.Quantity)
                    throw new NotEnoughStockException("Недостаточно товара на складе");

                product.Stock -= orderProduct.Quantity;
            }
        }
    }
}
