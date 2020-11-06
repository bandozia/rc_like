using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCLike.Data.Service
{
    public class TokenService : ITokenService
    {
        private readonly string _secret;

        public TokenService(string secret)
        {
            _secret = secret;
        }

        public string DecodeToken(string token)
        {
            var tokenHandle = new JsonWebTokenHandler();
            var result = tokenHandle.ValidateToken(token, ValidationParameters());
            
            if (result.IsValid && result.Claims.ContainsKey("email"))
                return result.Claims.SingleOrDefault(c => c.Key == "email").Value.ToString();
            else            
                return null;
                        
        }

        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(_secret);
            var tokenHandle = new JsonWebTokenHandler();

            var claims = new Dictionary<string, object>
            {
                {"email", email}
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
                        
            return tokenHandle.CreateToken(tokenDescriptor);
        }                           

        private TokenValidationParameters ValidationParameters() =>
            new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret))
            };
    }
}
