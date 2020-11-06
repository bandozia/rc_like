using System;
using System.Collections.Generic;
using System.Text;

namespace RCLike.Data.Models
{
    public class UrlSource : BaseModel
    {
        public string Url { get; set; }

        public virtual ICollection<AppUser> UsersWhoLiked { get; private set; }

        public void AddUserWhoLiked(AppUser user)
        {
            UsersWhoLiked = UsersWhoLiked ?? new HashSet<AppUser>();
            UsersWhoLiked.Add(user);                        
        }
    }
}
