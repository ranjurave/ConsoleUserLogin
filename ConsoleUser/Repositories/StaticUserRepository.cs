using ConsoleUser.Models;

namespace ConsoleUser.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                UserName = "jo", Email = "jo@gmail.com",
                Id = Guid. NewGuid(), Password = "string"
            }
        };

        public async Task<User> AuthenticateAsync(string useremail, string password)
        {
            var user = Users.Find(x => x.Email.Equals(useremail, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);
            if(user != null)
            {
                return user;
            }
            else
            {
                return user;
            }
        }
    }
}
