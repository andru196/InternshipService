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
	public class UniversityController : ControllerBasePlusAuth
	{
		public UniversityController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UniversityDto>> Get(Guid id) => Utils.IsNull(
			await _dbContext.Universities
			.Include(x=>x.Avatar)
			.FirstOrDefaultAsync(x => x.Guid == id), out var university)
			? NotFound()
			: Ok(new UniversityDto(university));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<UniversityDto>>> Get(string? text = null, int page = 1, int pageSize = 10, EntityType[] types = null)
			=> Ok(_dbContext.Universities.Include(x => x.Avatar)
			.WhereNotNull(text, x=> x.Name.Contains(text) || x.City.Contains(text) || x.Address.Contains(text))
				.TakePage(page, pageSize)
				.Select(x=> new UniversityDto(x, types)));
			

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<UniversityDto>> Post(UniversityDto universityDto)
		{
			var university = _mapper.Map<University>(universityDto);
			_dbContext.Add(university);
			await _dbContext.SaveChangesAsync();
			return (Ok(new UniversityDto(university)));
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UniversityDto>> Put(UniversityDto universityDto)
		{
			var uniDb = await _dbContext.Universities.FirstOrDefaultAsync(x => x.Guid == universityDto.Guid);
			if (uniDb == null)
				return NotFound();
			var university = _mapper.Map<InternResponse>(universityDto);
			university.Id = uniDb.Id;
			_dbContext.Update(university);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

	}
}
