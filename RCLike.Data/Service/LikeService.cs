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
        private readonly ITokenService _tokenService;
        private readonly ILikerReository _likerReository;

        public LikeService(IUrlRepository urlRepository, ITokenService tokenService, ILikerReository likerReository)
        {
            _urlRepository = urlRepository;
            _tokenService = tokenService;
            _likerReository = likerReository;
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
        }

        public async Task<int> GetLikeCount(string url)
        {
            //TODO: load from cashe cache if exists
            var urlSource = await _urlRepository.GetByUrlAsync(url);
            if (urlSource != null)
                return urlSource.Likers?.Count ?? 0;
            else
                return -1;
        }
    }
}
