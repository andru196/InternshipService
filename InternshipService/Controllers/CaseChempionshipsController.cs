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
	[Route("api/[controller]")]
	[ApiController]
	public class CaseChempionshipsController : ControllerBasePlusAuth
	{
		public CaseChempionshipsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<CaseChempionshipDto>> Get(Guid id) => Utils.IsNull(await _dbContext.CaseChempionships.FirstOrDefaultAsync(x=>x.Guid == id), out var championship)
			? NotFound() : Ok(new CaseChempionshipDto(championship!));


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<CaseChempionshipDto>>> Get(string? name = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.CaseChempionships.WhereNotNull(name, x => x.Name.Contains(name))
			.TakePage(page, pageSize)
			.Select(x=> new CaseChempionshipDto(x)));

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<CaseChempionshipDto>> Post(CaseChempionshipDto caseChempionshipDto)
		{
			var caseChempionship = await PostAsync<CaseChempionship, CaseChempionshipDto>(caseChempionshipDto);
			return Ok(new CaseChempionshipDto(caseChempionship));
		}

		[Authorize(Roles = $"{nameof(UserType.Admin)},{nameof(UserType.Mentor)}")]
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<CaseChempionshipDto>> Put(CaseChempionshipDto caseChempionshipDto)
		{
			var caseChempionshipDb = await _dbContext.CaseChempionships.FirstOrDefaultFromDtoAsync(caseChempionshipDto);
			var caseChempionship = _mapper.Map<UserTraining>(caseChempionshipDto);
			caseChempionship.Id = caseChempionshipDb.Id;
			_dbContext.UserTrainings.Update(caseChempionship);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("interns/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternCaseChempionshipResultDto>> GetInternCaseChempionshipResult(Guid id) => Utils.IsNull(
			await _dbContext.InternCaseChempionshipResults.FirstOrDefaultAsync(x=>x.Guid == id),
			out var result) ? NotFound()
			: Ok(new InternCaseChempionshipResultDto(result));


		[HttpGet("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<InternCaseChempionshipResultDto>>> GetInternCaseChempionshipResults(Guid? internId = null, Guid? caseChempionshipId = null,
																										int page = 1, int pageSize = 10) =>
			Ok(_dbContext.InternCaseChempionshipResults
				.WhereNotNull(caseChempionshipId, x => x.CaseChempionshipId == caseChempionshipId)
				.WhereNotNull(internId, x => x.InternId == internId)
				.TakePage(page, pageSize)
				.Select(x => new InternCaseChempionshipResultDto(x))
				);

		[HttpPost("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<InternCaseChempionshipResultDto>> PostGetInternCaseChempionshipResult(InternCaseChempionshipResultDto internCaseChempionshipResultDto)
		{
			var db = await PostAsync<InternCaseChempionshipResult, InternCaseChempionshipResultDto>(internCaseChempionshipResultDto);
			return Ok(db);
		}


		[HttpPut("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> PutGetInternCaseChempionshipResult(InternCaseChempionshipResultDto internCaseChempionshipResultDto)
		{
			var db = await _dbContext.InternCaseChempionshipResults.FirstOrDefaultFromDtoAsync(internCaseChempionshipResultDto);
			if (db == null)
				return NotFound();
			var result = _mapper.Map<InternResponse>(internCaseChempionshipResultDto);
			result.Id = db.Id;
			_dbContext.Update(result);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
