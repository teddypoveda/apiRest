using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Entities;
using WebApi.Domain.Exceptions;
using WebApi.Domain.Interfaces;
using WebApi.Domain.Interfaces.Services;
using WebApi.Services;

namespace WebApi.WebApi.Controllers
{
    [ApiController]
    [Route("/api/test")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IPasswordService _passwordService;
        private readonly UsuarioService _usuarioService;

        public TestController(ITestService testService,IPasswordService passwordService,UsuarioService usuarioService)
        {
            _testService = testService;
            _passwordService = passwordService;
            _usuarioService = usuarioService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> Test([FromQuery] PagedQuery pagedQuery)
        {
            /*throw new ApiException("Hola", HttpStatusCode.Conflict);*/
            var data = await _testService.GetAll(pagedQuery.pageNumber,pagedQuery.pageSize,"");
            Metadata metadata = new Metadata()
            {
                TotalCount=data.TotalCount,
                PageSize=data.PageSize,
                CurrentPage=data.CurrentPage,
                TotalPages=data.TotalPages,
                HasNextPage=data.HasNextPage,
                HasPreviousPage=data.HasPreviousPage
            };
            return Ok(new{data,metadata});
        }

        [HttpGet("/error")]
        public async Task<ActionResult<AuthToken>> Error()
        {
            return Ok(await _usuarioService.Test());
        }
    }
}