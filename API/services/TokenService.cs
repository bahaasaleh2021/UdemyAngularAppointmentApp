using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.services
{
    public class TokenService : ITokenService
    {
        public IConfiguration _conf { get; }
        public SymmetricSecurityKey _key { get; set; }
        public TokenService(IConfiguration conf)
        {
            _conf = conf;
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Secrets:JWTTokenKey"]));

        }

        public string CreateToken(AppUser user)
        {
            var claims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };

            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDesc=new SecurityTokenDescriptor{
                 Subject=new ClaimsIdentity(claims),
                 Expires=DateTime.Now.AddDays(7),
                 SigningCredentials=creds
            };

            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}