namespace Guestbook.Repository
{
    public static class GuestbookRepositoryExtensions
    {
        public static void AddGuestbookRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository,  GuestbookRepository>();
        }
    }
}
