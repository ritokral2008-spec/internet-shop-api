using InternetShop.DTOs.Products;
using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseProductDto> Add(CreateProductDto product);
        Task<ResponseProductDto> GetById(int id);
        Task Remove(int id);
        Task<IEnumerable<ResponseProductDto>> GetAll();
        Task<ResponseProductDto> Update(int id, UpdateProductDto product);
    }
}
