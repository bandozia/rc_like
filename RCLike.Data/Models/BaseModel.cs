using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RCLike.Data.Models
{
    public abstract class BaseModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
