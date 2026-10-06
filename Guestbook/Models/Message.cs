namespace Guestbook.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public string? Date { get; set; }
        public User? User { get; set; }
    }
}
