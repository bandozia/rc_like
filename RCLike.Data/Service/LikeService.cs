using RCLike.Data.Models;
using RCLike.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;

namespace RCLike.Data.Service
{
    public class LikeService : ILikeService
    {

        private readonly IUrlRepository _urlRepository;
        private readonly ITokenService _tokenService;
        private readonly ILikerReository _likerReository;
        private readonly IDistributedCache _cache;

        public LikeService(IUrlRepository urlRepository,
            ITokenService tokenService,
            ILikerReository likerReository,
            IDistributedCache cache)
        {
            _urlRepository = urlRepository;
            _tokenService = tokenService;
            _likerReository = likerReository;
            _cache = cache;
        }

        public async Task<int> GetLikeCount(string url)
        {
            string cached = await _cache.GetStringAsync(url);
            if (string.IsNullOrEmpty(cached))
            {
                var urlSource = await _urlRepository.GetByUrlAsync(url);
                if (urlSource != null)
                {
                    int count = urlSource.Likers?.Count ?? 0;
                    await _cache.SetStringAsync(url, count.ToString());
                    return count;
                }
                else
                    return -1;
            }
            else
            {
                int count = int.Parse(cached);                
                return count;
            }                        
        }

        public async Task DoLike(string url, string token)
        {
            string email = _tokenService.DecodeToken(token);

            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("invalid token");

            var liker = await _likerReository.GetByEmailAsync(email) ?? new Liker { Email = email };

            if (liker.HasLiked(url))
                throw new UnauthorizedAccessException("user already liked this url");

            var urlSource = await _urlRepository.GetByUrlAsync(url);

            if (urlSource != null)
            {
                urlSource.AddUserWhoLiked(liker);
                await _urlRepository.UpdateAsync(urlSource);                
            }
            else
            {
                var newUrl = new UrlSource { Url = url };
                newUrl.AddUserWhoLiked(liker);
                await _urlRepository.InsertAsync(newUrl);
            }
            
            await _cache.RemoveAsync(url);
        }
               
        public async Task<LikerValidation> VlidateLiker(string token, string url)
        {
            var likerEmail = _tokenService.DecodeToken(token);
            if (likerEmail != null)
            {
                var liker = await _likerReository.GetByEmailAsync(likerEmail);                
                return new LikerValidation
                {
                    IsValid = true,
                    HasLiked = liker != null ? liker.HasLiked(url) : false
                };
            }
            else
            {
                return new LikerValidation { IsValid = false };
            }

        }


    }
}
