using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces.Services;

namespace WebApi.Services
{
    public class UsuarioService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public UsuarioService(UserManager<User> userManager,IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthToken> Test()
        {
            
            var usuair = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == 1);
            var test = await _userManager.GetRolesAsync(usuair);
            List<Claim> claims = new List<Claim>() { };
            foreach (var rol in test)
            {
                claims.Add(new Claim(ClaimTypes.Role,rol));   
            }
            var token =  _jwtService.GenerateToken(usuair,claims);
            return new AuthToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = token.ValidTo
            };
        }
    }
}