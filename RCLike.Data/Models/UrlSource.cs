using System;
using System.Collections.Generic;
using System.Text;

namespace RCLike.Data.Models
{
    public class UrlSource : BaseModel
    {
        public string Url { get; set; }

        public virtual ICollection<Liker> Likers { get; private set; }

        public void AddUserWhoLiked(Liker liker)
        {
            Likers = Likers ?? new HashSet<Liker>();
            Likers.Add(liker);                        
        }
              
    }
}
