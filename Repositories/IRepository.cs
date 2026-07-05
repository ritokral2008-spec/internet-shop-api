namespace InternetShop.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);
        Task<T> GetById(int id);
        void Remove(int id);
        Task<IEnumerable<T?>> GetAll();
    }
}
