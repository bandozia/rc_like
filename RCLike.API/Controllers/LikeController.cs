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
        private readonly IUserReository __userRepo;

        public LikeController(ILikeService likeService, IUserReository userRepo)
        {
            _likeService = likeService;
            __userRepo = userRepo;
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


        [HttpPost]
        public async Task<IActionResult> Like(string url, [FromHeader] string token)
        {
            //test
            var user = await __userRepo.GetByIdEmailAsync("test@user.com");

            await _likeService.DoLike(url, user);
            return Ok(token);
        }
                
    }
}
