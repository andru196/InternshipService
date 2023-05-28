using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using InternshipService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBasePlusAuth
	{
		public UsersController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }


		[Authorize]
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserDto>> Get(Guid id) => Utils.IsNull(
			await _dbContext.Users.FilterByUser(Identity)
			.FirstOrDefaultAsync(x => x.Guid == id), out var user)
			? NotFound()
			: Ok(new UserDto(user));

		[Authorize]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<UserDto>>> Get(string name = null, UserType? userType = null, int page = 1, int pageSize = 10) => 
			Ok(_dbContext.Users
			.WhereNotNull(name, x=> x.FirstName.Contains(name) || x.SecondName.Contains(name) || x.MiddleName.Contains(name))
			.WhereNotNull(userType, x=> x.Type == userType)
			.FilterByUser(Identity)
			.TakePage(page, pageSize)
			.Select(x=> new UserDto(x)));

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<UserDto>> Post(UserDto userDto)
		{
			if (User.Identity.IsAuthenticated && Identity.Role != UserType.Admin)
				return Unauthorized();
			var user = _mapper.Map<User>(userDto);
			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();
			return Ok(user);
		}

		/// <summary>
		///  Регистрация прибытия из канала
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <param name="channelName">Откуда пришёл</param>
		/// <returns></returns>
		[HttpPost("FromChannel")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<UserDto>> Post(Guid userId, string channelName="site")
		{
			var arrival = new ArrivalsFromChannel
			{
				Guid = new Guid(),
				Channel = channelName,
				UserId = userId
			};
			_dbContext.Arrivals.Add(arrival);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[Authorize]
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(UserDto userDto)
		{
			var userDb = await _dbContext.Users.FilterByUser(Identity).FirstOrDefaultAsync(x => x.Guid == userDto.Id);
			if (userDb == null) return NotFound();
			var user = _mapper.Map<User>(userDto);
			user.Id = userDb.Id;
			_dbContext.Users.Update(user);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[Authorize]
		[HttpGet("me")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserDto>> GetMe()
		 => Ok(!Utils.IsNull(_dbContext.Users.First(x => x.Guid == new Guid(Identity.UserId)), out var user) && user.Type == UserType.Admin ?
			 new UserDto(user) : user.Type == UserType.Student ?
			 new UserInternDto(user, new InternDto(_dbContext.Interns.First(x => x.UserId == user.Guid))) : user.Type == UserType.Mentor ?
			 new UserMentorDto(user, new MentorDto(_dbContext.Mentors.First(x => x.UserId == user.Guid))) : user.Type == UserType.Buddy ?
			 new UserBuddyDto(user, new BuddyDto(_dbContext.Buddies.First(x => x.UserId == user.Guid))) :
			 new UserOrganizationAdminDto(user, new OrganizationAdminDto(_dbContext.OrganizationAdmins.First(x => x.UserId == user.Guid))));

	}
}
