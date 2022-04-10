using System;

namespace WebApi.Domain.CustomGenerator
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}