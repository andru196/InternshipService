using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using InternshipService.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBasePlusAuth
	{
		public UsersController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserDto>> Get(Guid id) => Utils.IsNull(
			await _dbContext.Users
			.FirstOrDefaultAsync(x => x.Guid == id), out var user)
			? NotFound()
			: Ok(new UserDto(user));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<UserDto>>> Get(string name = null, UserType? userType = null, int page = 1, int pageSize = 10) => 
			Ok(_dbContext.Users
			.WhereNotNull(name, x=> x.FirstName.Contains(name) || x.SecondName.Contains(name) || x.MiddleName.Contains(name))
			.WhereNotNull(userType, x=> x.Type == userType)
			.TakePage(page, pageSize)
			.Select(x=> new UserDto(x)));

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<UserDto>> Post(UserDto userDto)
		{
			var user = _mapper.Map<User>(userDto);
			_dbContext.Users.Add(user);
			await _dbContext.SaveChangesAsync();
			return Ok(user);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(UserDto userDto)
		{
			var userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Guid == userDto.Id);
			if (userDb == null) return NotFound();
			var user = _mapper.Map<User>(userDto);
			user.Id = userDb.Id;
			_dbContext.Users.Update(user);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

	}
}
