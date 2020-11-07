using Microsoft.AspNetCore.Mvc;
using RCLike.Data.Models;
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
                
        public LikeController(ILikeService likeService)
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

        [HttpGet]
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

        [HttpGet("validate-liker")]
        public async Task<LikerValidation> ValidateLiker([FromQuery] string url, [FromQuery] string token)
        {
            return await _likeService.VlidateLiker(token, url);
        }
                
                
    }
}
