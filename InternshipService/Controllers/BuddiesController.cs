using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using InternshipService.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuddiesController : ControllerBasePlusAuth
	{
		public BuddiesController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<BuddyDto>> Get(Guid id) => Utils.IsNull(await _dbContext.Buddies.FirstOrDefaultAsync(x=>x.Guid == id), out var buddy)
			? NotFound() : Ok(buddy);


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<BuddyDto>>> Get(Guid? organizationId = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.Buddies.WhereNotNull(organizationId, x => x.OrganizationId == organizationId)
			.TakePage(page, pageSize)
			.Select(x => new BuddyDto(x))
			);

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<BuddyDto>> Post(BuddyDto buddyDto)
		{
			var buddy = await PostAsync<Buddy, BuddyDto>(buddyDto);
			return Ok(new BuddyDto(buddy));
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(BuddyDto buddyDto)
		{
			var db = await _dbContext.Buddies.FirstOrDefaultFromDtoAsync(buddyDto);
			if (db == null) return NotFound();
			var buddy = _mapper.Map<Buddy>(buddyDto);
			buddy.Id = db.Id;
			_dbContext.Buddies.Update(buddy);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}


		[HttpGet("interns/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<BuddyInternLinkDto>> GetIntern(Guid id) => Utils.IsNull(await _dbContext.BuddiesLink.FirstOrDefaultAsync(x => x.Guid == id), out var link)
			? NotFound() : Ok(new BuddyInternLinkDto(link));


		[HttpGet("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<BuddyInternLinkDto>>> GetIntern(Guid? buddyId = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.BuddiesLink
			.WhereNotNull(buddyId, x => x.BuddyId == buddyId)
			.TakePage(page, pageSize)
			.Select(x=> new BuddyInternLinkDto(x))
			);

		[HttpPost("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<BuddyInternLinkDto>> PostIntern(BuddyInternLinkDto buddyDto)
		{
			var link = await PostAsync<BuddyInternLink, BuddyInternLinkDto>(buddyDto);
			return Ok(link);
		}


		[HttpPut("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> PutIntern(BuddyInternLinkDto link)
		{
			var db = _dbContext.BuddyInternLinks.FirstOrDefaultFromDtoAsync(link);
			if (db == null) return NotFound();
			var buddy = _mapper.Map<BuddyInternLink>(link);
			buddy.Id = db.Id;
			_dbContext.BuddyInternLinks.Update(buddy);
			return Ok();
		}
	}
}
