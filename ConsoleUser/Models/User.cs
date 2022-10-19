namespace ConsoleUser.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
