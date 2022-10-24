using ConsoleUser.Models;

namespace ConsoleUser.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string useremail, string password);
    }
}
