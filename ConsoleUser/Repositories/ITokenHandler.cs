using ConsoleUser.Models;

namespace ConsoleUser.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
