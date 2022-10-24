using ConsoleUser.Data;
using ConsoleUser.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUser.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersAPIDbContext usersAPIDbContext;

        public UserRepository(UsersAPIDbContext usersAPIDbContext)
        {
            this.usersAPIDbContext = usersAPIDbContext;
        }
        public async Task<User> AuthenticateAsync(string useremail, string password)
        {
            var user = await usersAPIDbContext.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == useremail.ToLower() && x.Password == password);
            if(user == null)
            {
                return null;
            }
            return user;

        }
    }
}
