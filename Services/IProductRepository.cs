using InternetShop.Repositories;

namespace InternetShop.Services
{
    public interface IProductRepository<T>: IRepository<T>
    {
        void Update(int id, T item);
    }
}
