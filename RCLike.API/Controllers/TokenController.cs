using Microsoft.AspNetCore.Mvc;
using RCLike.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCLike.API.Controllers
{    
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("generate-token")]
        public string GenerateArbitrayToken([FromQuery] string email)
        {
            return _tokenService.GenerateToken(email);
        }
    }
}
