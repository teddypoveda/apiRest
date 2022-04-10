using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        JwtSecurityToken GenerateToken(User user,List<Claim> additionalClaims = null);
    }
}