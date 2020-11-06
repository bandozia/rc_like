using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace RCLike.Data.Models
{
    public class Liker : BaseModel
    {
        [MaxLength(200)]
        [Required]
        public string Email { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<UrlSource> LikedUrls { get; private set; }

        public bool HasLiked(string url)
        {
            return LikedUrls?.SingleOrDefault(u => u.Url == url) != null;
        }
                
    }
}
