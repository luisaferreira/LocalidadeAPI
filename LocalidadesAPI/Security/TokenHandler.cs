using LocalidadesAPI.Interfaces.Security;
using LocalidadesAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocalidadesAPI.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _Configuration;

        public TokenHandler(IConfiguration configuration)
            => _Configuration = configuration;

        public Task<string> GerarToken(Usuario usuario)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credenciais);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
