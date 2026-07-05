namespace InternetShop.Services
{
    public interface IProductService<T>
    {
        void Add(T item);
        Task<T> GetById(int id);
        void Remove(int id);
        Task<IEnumerable<T?>> GetAll();
        Task Update(int id, T item);
    }
}
