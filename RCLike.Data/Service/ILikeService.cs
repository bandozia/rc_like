using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.Data.Service
{
    public interface ILikeService
    {
        public Task DoLike(string url, AppUser user);
        public Task<int> GetLikeCount(string url);
        
    }
}
