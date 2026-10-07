namespace Guestbook.Repository
{
    using Guestbook.Models;
    public interface IRepository
    {
        Task<IEnumerable<Message>> GetMessagesToListAsync();
        Task<IEnumerable<User>> GetUsersToListAsync();
        User? GetUserByLogin(string login);
        Task AddMessageAsync(Message message);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
