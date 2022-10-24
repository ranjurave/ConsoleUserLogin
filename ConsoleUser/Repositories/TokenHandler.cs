using ConsoleUser.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConsoleUser.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            var claims =new List<Claim>();
            claims.Add(new Claim(ClaimTypes.UserData, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            //user.ForEach((role) =>
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //});
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                //claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
