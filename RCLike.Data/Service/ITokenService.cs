using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCLike.Data.Service
{
    public interface ITokenService
    {
        public string DecodeToken(string token);
        public string GenerateToken(string email);
    }
}
