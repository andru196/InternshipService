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
	public class LinksController : ControllerBasePlusAuth
	{
		public LinksController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<LinkDto>> Get(Guid id) => Utils.IsNull(await _dbContext.Links.FirstOrDefaultAsync(x => x.Guid == id), out var link)
			? NotFound() : Ok(link);


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<LinkDto>>> Get(Guid? forId = null, EntityType? type = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.Links
			.WhereNotNull(forId, x => x.ForId == forId)
			.WhereNotNull(type, x => x.EntityType == type)
			.TakePage(page, pageSize)
			.Select(x => new LinkDto(x))
			);

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<LinkDto>> Post(LinkDto linkDto)
		{
			var link = await PostAsync<Link, LinkDto>(linkDto);
			return Ok(new LinkDto(link));
		}
	}
}
