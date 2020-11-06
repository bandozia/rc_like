using Microsoft.EntityFrameworkCore;
using RCLike.Data.Contexts;
using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.Data.Repositories.ef
{
    public class LikerRepository : BaseRepository<Liker>, ILikerReository
    {
        public LikerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Liker> GetByEmailAsync(string email)
        {
            return await DbSet.Include(u => u.LikedUrls).SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task Insert(Liker user)
        {
            DbSet.Add(user);
            await Context.SaveChangesAsync();
        }
    }
}
