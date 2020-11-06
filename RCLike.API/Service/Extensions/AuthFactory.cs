using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RCLike.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.API.Service.Extensions
{
    public static class AuthFactory
    {
        public static ITokenService JWTTokenService(IServiceProvider sp)
        {
            var config = sp.GetRequiredService<IConfiguration>();
            return new TokenService(config["token:secret"]);
        }
               
    }
}
