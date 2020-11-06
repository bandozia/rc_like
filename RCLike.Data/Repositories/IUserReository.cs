using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.Data.Repositories
{
    public interface IUserReository
    {
        public Task<AppUser> GetByIdEmailAsync(string email);
        public Task Insert(AppUser user);
    }
}
