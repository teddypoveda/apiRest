using System;

namespace WebApi.Domain.CustomGenerator
{
    public class JwtServiceOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double TokenTimeoutMinutes { get; set; }
    }
}