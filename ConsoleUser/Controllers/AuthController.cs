using Microsoft.AspNetCore.Mvc;
using ConsoleUser.DTO;
using ConsoleUser.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await userRepository.AuthenticateAsync(loginRequest.UserEmail, loginRequest.Password);
            if (user !=null)
            {
                //generate token
                var token = await  tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("incorrect fields");
        }
    }
}
