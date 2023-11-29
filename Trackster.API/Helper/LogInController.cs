﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Trackster.API.Helper
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LogInController : ControllerBase
    {
        private readonly TracksterContext dbContext;
        private readonly UserService userService;


        public LogInController(TracksterContext dbContext, UserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }


        private RegisteredUser ProvjeraEmail(string email)
        {
            var useremail = dbContext.RegisteredUsers.FirstOrDefault(x => x.Email == email);
            return useremail;
        }
        private string PasswordChecker(string password)
        {
            var HashPassword = PasswordHelper.Hash(password);
            return HashPassword;
        }

        [HttpPost]
        public IResult LogInAuth(UserLogInVM x)
        {

            #region UserAuth

            var User = ProvjeraEmail(x.Email);
            if (User == null)
                return Results.BadRequest("Email");

            string? UserPassword = PasswordChecker(x.Password);
            if (User.Password != UserPassword)
                return Results.BadRequest("Password");

            #endregion

            #region JWT Token generation

            var builder = WebApplication.CreateBuilder();

            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, User.RegisteredUserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(TokenDescriptor);
            var jwtToken = tokenhandler.WriteToken(token);

            return Results.Ok(jwtToken);

            #endregion

        }

        [HttpGet]
        public ActionResult<object> GetLoggedInUser()
        {
            var user = userService.GetLoggedInUser(HttpContext);
            return user == null ? Accepted() :
                Ok(new
                {
                    username = user.ToString(),
                    userId = user.RegisteredUserId,
                    picture = user.Picture
                }
                );
        }

    }
}