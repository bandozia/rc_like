using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RCLike.Data.Models
{
    public class AppUser : BaseModel
    {
        [MaxLength(200)]
        [Required]
        public string Email { get; set; }
               
        public virtual ICollection<UrlSource> LikedUrls { get; private set; }
                
    }
}
