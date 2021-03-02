using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ALBaB.Entities;
using ALBaB.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ALBaB.Token
{
    public class TokenService : ITokenService // our interface
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)

        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

        }

        public async Task<String> CreateToken(AppUser user)
        {
            var claims = new List<Claim> // not sensitive information
            {
              new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
              new Claim(JwtRegisteredClaimNames.GivenName,user.DisplayName),
              new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user); //all roles of user
           
            // we dont user JwtRegisteredClaimNames because no options of Role has
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));
            

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();
           
            var token = tokenHandler.CreateToken(tokenDesciptor); // creating token

            return tokenHandler.WriteToken(token);

        }
    }
}