using Microsoft.EntityFrameworkCore;
using RCLike.Data.Contexts;
using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.Data.Repositories.ef
{
    public class UrlRepository : BaseRepository<UrlSource>, IUrlRepository
    {
        public UrlRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UrlSource> GetByUrlAsync(string url)
        {
            return await DbSet.Include(u => u.UsersWhoLiked).SingleOrDefaultAsync(u => u.Url == url);            
        }

        public async Task InsertAsync(UrlSource url)
        {
            DbSet.Add(url);
            await Context.SaveChangesAsync();            
        }

        public Task UpdateAsync(UrlSource url)
        {
            throw new NotImplementedException();
        }
    }
}
