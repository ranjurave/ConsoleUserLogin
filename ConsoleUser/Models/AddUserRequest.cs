using System.Diagnostics.CodeAnalysis;

namespace ConsoleUser.Models
{
    public class AddUserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
