namespace InternetShop.Services
{
    public interface IOrderService<T>
    {
        Task CreateOrder(T order);
        Task<IEnumerable<T?>> GetAll();
        Task<T> GetById(int id);
        void Remove(int id);
    }
}
