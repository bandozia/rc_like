using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCLike.Data.Repositories
{
    public interface IUrlRepository
    {
        public Task InsertAsync(UrlSource url);
        public Task<UrlSource> GetByUrlAsync(string url);
        public Task UpdateAsync(UrlSource url);
    }
}
