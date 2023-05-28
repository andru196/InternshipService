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
	public class InternshipsController : ControllerBasePlusAuth
	{
		public InternshipsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}

		[HttpGet("intern/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternsInternshipDto>> Get(Guid id) => Utils.IsNull(await _dbContext.InternsInternships.FirstOrDefaultAsync(x => x.Guid == id), out var internship)
			? NotFound() : Ok(internship);


		[HttpGet("intern")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<InternsInternshipDto>>> Get(Guid? organizationId = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.InternsInternships
			.WhereNotNull(organizationId, x=>x.PartOfInternRequest.OrganizationId == organizationId)
			.TakePage(page, pageSize)
			.Select(x => new InternsInternshipDto(x))
			);

		[HttpPost("intern")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<InternsInternshipDto>> Post(InternsInternshipDto internshipDto)
		{
			var internship = await PostAsync<InternsInternship, InternsInternshipDto>(internshipDto);
			return Ok(new InternsInternshipDto(internship));
		}


		[HttpPut("intern")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternsInternshipDto>> Put(InternsInternshipDto internshipDto)
		{
			var db = await _dbContext.InternsInternships.FirstOrDefaultFromDtoAsync(internshipDto);
			if (db == null) return NotFound();
			var internship = _mapper.Map<InternsInternship>(internshipDto);
			internship.Id = db.Id;
			_dbContext.InternsInternships.Update(internship);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
