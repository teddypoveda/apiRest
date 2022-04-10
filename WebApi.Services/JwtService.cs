using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JwtServiceOptions> _options;

        public JwtService(IOptions<JwtServiceOptions> options)
        {
            _options = options;
        }

        public JwtSecurityToken GenerateToken(User user, List<Claim> additionalClaims = null)
        {
            List<Claim> claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),

                new (JwtRegisteredClaimNames.Email,
                    user.Email),

                new (JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),
            };

            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                expires: DateTime.UtcNow.AddMinutes(_options.Value.TokenTimeoutMinutes),
                claims: claims,
                signingCredentials: creds
            );
        }
    }
}