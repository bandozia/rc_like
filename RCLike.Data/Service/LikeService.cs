using RCLike.Data.Models;
using RCLike.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RCLike.Data.Service
{
    public class LikeService : ILikeService
    {

        private readonly IUrlRepository _urlRepository;

        public LikeService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task DoLike(string url, AppUser user)
        {
            //TODO: validate url before insert
            //TODO: check if user already liked
            

            var urlSource = await _urlRepository.GetByUrlAsync(url);
            if (urlSource != null)
            {
                urlSource.AddUserWhoLiked(user);
                await _urlRepository.UpdateAsync(urlSource);
            }
            else
            {
                var newUrl = new UrlSource { Url = url };
                newUrl.AddUserWhoLiked(user);
                await _urlRepository.InsertAsync(newUrl);
            }
        }

        public async Task<int> GetLikeCount(string url)
        {
            //TODO: load from cashe cache if exists
            var urlSource = await _urlRepository.GetByUrlAsync(url);
            if (urlSource != null)
                return urlSource.UsersWhoLiked?.Count ?? 0;
            else
                return -1;
        }
    }
}
