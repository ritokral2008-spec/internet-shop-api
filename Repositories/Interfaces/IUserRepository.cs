using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetById(int id);
        Task<User?> GetByUsername(string username);
        Task Remove(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> Update(int id, User user);
    }
}
