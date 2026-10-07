namespace Guestbook.Repository
{
    using Guestbook.Models;
    using Microsoft.EntityFrameworkCore;

    public class GuestbookRepository(GuestbookContext _db) : IRepository
    {
        public async Task<IEnumerable<Message>> GetMessagesToListAsync()
        {
            return await _db.Messages.Include(m => m.User).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersToListAsync()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public User? GetUserByLogin(string login)
        {
            return _db.Users.FirstOrDefault(u => u.Login == login);
        }

        public async Task AddMessageAsync(Message message)
        {
            await _db.Messages.AddAsync(message);
        }

        public async Task AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
