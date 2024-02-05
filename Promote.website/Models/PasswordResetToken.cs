namespace Promote.website.Models
{
    public class PasswordResetToken
    {
        public int Id { get; set; }

        public int? AdminId { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
