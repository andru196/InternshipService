using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InternshipService.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TagsController : ControllerBasePlusAuth
	{
		public TagsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<TagDto>> Get(Guid id) => Utils.IsNull(
			await _dbContext.Tags
			.FirstOrDefaultAsync(x => x.Guid == id), out var tag)
			? NotFound()
			: Ok(new TagDto(tag));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<TagDto>>> Get(string text, int page = 1, int pageSize = 10)
		{
			if (text.Length < 5)
			{
				var pattern = $"%{string.Join('%', text.Split(""))}%";
				var rez = _dbContext.Tags.Where(x => EF.Functions.Like(x.Name, pattern)).Select(x => new TagDto(x));
				return new ActionResult<IEnumerable<TagDto>>(rez.ToList());
			}
			else
				return new ActionResult<IEnumerable<TagDto>>(_dbContext.Tags
					.Where(x => x.Name.Contains(text))
					.Skip(page * pageSize - pageSize)
					.Take(pageSize)
					.Select(x => new TagDto(x)).ToList());
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<TagDto>> Post(TagDto tagDto)
		{
			var tag = await PostAsync<Tag, TagDto>(tagDto);
			return Ok(tag);
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<TagDto>> Put(TagDto tagDbo)
		{
			var tagDb = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Guid == tagDbo.Guid);
			if (tagDb == null)
				return NotFound();
			var tag = _mapper.Map<Tag>(tagDbo);
			tag.Id = tagDb.Id;
			_dbContext.Update(tag);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

	}
}
