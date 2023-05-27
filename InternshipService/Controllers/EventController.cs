using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using InternshipService.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventController : ControllerBasePlusAuth
	{
		public EventController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<EventDto>> Get(Guid id)
		{
			var @event = _dbContext.Events.FirstOrDefault(x => x.Guid == id);
			if (@event == null) return NotFound();
			return Ok(@event);
		
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<EventDto>>> Get(string text = null, Guid? organizationId = null, int page = 1, int pageSize = 10) =>
			Ok(_dbContext.Events
				.WhereNotNull(text, x=> x.Description.Contains(text))
				.WhereNotNull(organizationId, x=> x.ByOrganizationId == organizationId)
				.TakePage(page, pageSize));


		
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<EventDto>> Post(EventDto eventDto)
		{
			var @event = _mapper.Map<Event>(eventDto);
			@event.Guid = new Guid();
			@event.Id = 0;
			_dbContext.Add(eventDto);
			_dbContext.SaveChanges();
			return Created(@event.Guid.ToString(), new EventDto(@event));
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(EventDto eventDto)
		{
			var @event = _dbContext.Events.AsNoTracking().FirstOrDefault(x => x.Guid == eventDto.Id) ?? throw new HttpException(System.Net.HttpStatusCode.NotFound);
			var newVersion = _mapper.Map<Event>(eventDto);
			newVersion.Id = @event.Id;
			_dbContext.Events.Update(newVersion);
			await  _dbContext.SaveChangesAsync();
			return Ok();
		}

	}
}
