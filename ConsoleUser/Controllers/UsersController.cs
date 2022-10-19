using ConsoleUser.Data;
using ConsoleUser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersAPIDbContext dbContext;

        public UsersController(UsersAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest AddUserRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = AddUserRequest.UserName,
                Email = AddUserRequest.Email,
                Password = AddUserRequest.Password
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.Users.FindAsync(id);
            if(user != null)
            {
                user.UserName = updateUserRequest.UserName;
                user.Email = updateUserRequest.Email;
                user.Password = updateUserRequest.Password;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                dbContext.Remove(user);
                dbContext.SaveChangesAsync();
                return Ok("Contact deleted");
            }
            return NotFound();
        }
    }
}
