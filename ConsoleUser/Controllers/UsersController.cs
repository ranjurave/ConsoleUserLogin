using ConsoleUser.Data;
using ConsoleUser.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConsoleUser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersAPIDbContext dbContext;
        private readonly IConfiguration _configuration;

        public UsersController(UsersAPIDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
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

        //[HttpPost]
        //[Route("login/{email}")]
        //public async Task<IActionResult> UserLogin([FromRoute] string email, LoginUserRequest loginUserRequest)
        //{
        //    var user = dbContext.Users.Where(e => e.Email == email).FirstOrDefault(); 
        //    if (user == null)
        //    {
        //        return NotFound("email not found");
        //    }
        //    if (user.Password == loginUserRequest.Password)
        //    {
        //        string token = CreateToken(user);
        //        return Ok(token);
        //    }

        //    return BadRequest("wrong password");
        //}

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                dbContext.Remove(user);
                dbContext.SaveChanges();
                return Ok("Contact deleted");
            }
            return NotFound();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
