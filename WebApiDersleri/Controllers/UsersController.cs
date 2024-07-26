using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using WebApiDersleri.DTO;
using WebApiDersleri.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace WebApiDersleri.Controllers
{
	[ApiController]
	[Route("/api/controller")]
	public class UsersController:ControllerBase
	{
		private readonly UserManager<AppUsers> _userManager;
		private readonly SignInManager<AppUsers> _signInManager;
		private readonly IConfiguration _configuraton;
		public UsersController(UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager, IConfiguration configuraton)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_configuraton = configuraton;
		}

		[HttpPost("register")]
		public async Task<IActionResult> CreateUser(UserDTO model)
		{
            if (!ModelState.IsValid)
            {
				return BadRequest(ModelState);
            }
            var user = new AppUsers 
			{ 
			FullName = model.FullName,
			UserName = model.UserName, 
			Email = model.Email,
			DateAdded = DateTime.Now
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded) 
			{
				return StatusCode(201);
			}

			return BadRequest(result.Errors);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user == null) 
			{
				return BadRequest(new {message = "Email Hatalı!"});
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);

			if (result.Succeeded) 
			{
				return Ok(
					new {token = GenerateJWT(user)}
					);
			}
			return Unauthorized();
		}

		private object GenerateJWT(AppUsers user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuraton.GetSection("AppSettings:Secret").Value ?? "");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
						new Claim(ClaimTypes.Name, user.UserName ?? ""),
					}
				),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
