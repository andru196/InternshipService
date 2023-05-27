using DataModel.Context;
using DataModel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBasePlus
	{
		public AuthController(ILogger logger, InternshipsDbContect context) : base(logger, context, null)
		{}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult> Get(
#if DEBUG
			string login = "l@gi.n", string password = "123"
#else
			string login, string password
#endif
			)
		{
			var user = _dbContext.Users.FirstOrDefault(x => x.Email == login && x.Password == password);
			if (user == null)
				return Unauthorized();


			var claims = new List<Claim> { new Claim(ClaimTypes.Name, login),
											new Claim(ClaimTypes.Role, user.Type.ToString().ToLower()),
											new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString())
			};
			switch (user.Type)
			{
				case UserType.Mentor:
					var mentor = _dbContext.Mentors.First(x => x.UserId == user.Guid);
					claims.Add(new Claim("DirectionId", mentor.DirectionId.ToString()));
					break;
				case UserType.OrganizationAdmin:
					claims.Add(new Claim("OrganizationId", _dbContext.OrganizationAdmins.First(x=>x.UserId==user.Guid).OrganizationId.ToString()));
					break;
				default: break;
			}
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					claims: claims,
					expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
		}

		[Authorize]
		[HttpGet("logout")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Logout()
		{
			await this.HttpContext.SignOutAsync();
			return Ok();
		}
	}
}
