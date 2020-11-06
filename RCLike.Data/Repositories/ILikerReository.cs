using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.Data.Repositories
{
    public interface ILikerReository
    {
        public Task<Liker> GetByEmailAsync(string email);
        public Task Insert(Liker user);
    }
}
