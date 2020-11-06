using Microsoft.AspNetCore.Mvc;
using RCLike.Data.Repositories;
using RCLike.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCLike.API.Controllers
{

    [ApiController]
    [Route("api/like")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        
        public LikeController(ILikeService likeService, ITokenService tokenService)
        {
            _likeService = likeService;            
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetLikesCount([FromQuery] string url)
        {
            int count = await _likeService.GetLikeCount(url);
            if (count >= 0)
                return Ok(count);
            else 
                return NotFound();
        }


        [HttpGet("like")]
        public async Task<IActionResult> Like([FromQuery] string url, [FromQuery] string token)
        {
            try
            {
                await _likeService.DoLike(url, token);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

        }
                
    }
}
