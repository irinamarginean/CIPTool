using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CIPTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, 
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        [HttpPost("login/email")]
        public async Task<IActionResult> LoginByEmail([FromBody] EmailLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userManager.FindByEmailAsync(model.Email).Result is Associate user)
            {
                var result = await signInManager
                    .PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    var associate = userManager.Users.ToList().SingleOrDefault(r => r.Email == model.Email) as Associate;
                    var token = await GenerateJwtToken(model.Email, associate);

                    return Ok(new { token });
                }

                return BadRequest("Invalid login attempt!");
            }

            return NotFound("No such user could be found!");
        }

        [HttpPost("login/username")]
        public async Task<IActionResult> LoginByUsername([FromBody] UsernameLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userManager.FindByNameAsync(model.Username).Result is Associate user)
            {
                var result = await signInManager
                    .PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = userManager.Users.ToList().SingleOrDefault(r => r.UserName == model.Username) as Associate;
                    var token = await GenerateJwtToken(model.Username, appUser);

                    return Ok(new { token });
                }

                return BadRequest("Invalid login attempt!");
            }

            return NotFound("No such user could be found!");
        }

        private async Task<object> GenerateJwtToken(string email, Associate associate)
        {
            var roles = await userManager.GetRolesAsync(associate);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.GivenName, associate.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, associate.LastName),
                new Claim(ClaimTypes.NameIdentifier, associate.Id),
                new Claim(ClaimTypes.Email, email),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
