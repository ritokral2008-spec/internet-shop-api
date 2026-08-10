using InternetShop.Data;
using InternetShop.Exceptions;
using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
                throw new UserNotFoundException("Пользователь не найден");

            return user;
        }
        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task Remove(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null)
                throw new UserNotFoundException("Пользователь не найден");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> Update(int id, User user)
        {
            var existing = await _context.Users.FindAsync(id);

            if(existing == null)
                throw new UserNotFoundException("Пользователь не найден");

            existing.Username = user.Username;
            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;
            existing.Role = user.Role;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
