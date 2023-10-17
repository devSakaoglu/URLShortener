using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.API.Authentication;
using URLShortener.Domain.Entities;
using URLShortener.Domain.Enums;
using URLShortener.Shared.Models.AppUser;

namespace URLShortener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtProvider _jwtProvider;

        public AccountsController(UserManager<AppUser> userManager, JwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppUserModel model)
        {
            if (Request.HttpContext.User.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sid))
            {
                //user already logged in
                //return user to home page
                return Redirect("/");
            }

            var newUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var createUser = await _userManager.CreateAsync(newUser, model.Password);

            if (!createUser.Succeeded)
            {
                return BadRequest("Account couldn't be created.");
            }

            // TODO we should add IsDeleted == true here
            var currentUser = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == newUser.Email);

            if (currentUser is null)
            {
                return NotFound("User not found.");
            }

            var token = _jwtProvider.GenerateToken(currentUser);

            HttpContext.Response.Cookies.Append("JWT_TOKEN_COOKIE", token, new CookieOptions
            {
                HttpOnly = true,
                // SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(token);
        }

        [HttpDelete("{userId}")]
        [UserType(UserType.User)]
        public async Task<IActionResult> Delete(string userId)
        {
            var signedInUserId = GetUserIdFromJwtClaim();

            if (!string.Equals(signedInUserId.ToString(), userId))
            {
                return BadRequest("UserId doesn't match with the current signed in user.");
            }

            // TODO we should raise an event here for deleting AppUser's all links
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == signedInUserId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("User cannot be deleted.");
            }

            return Ok("User is deleted.");
        }
    }
}