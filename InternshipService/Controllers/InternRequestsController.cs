using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using InternshipService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InternRequestsController : ControllerBasePlusAuth
	{
		public InternRequestsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternRequestDto>> Get(Guid id) => Utils.IsNull(
			await _dbContext.InternRequests
			.Include(x=>x.Organization)
			.Include(x=>x.CreatedBy)
			.FilterByUser(Identity)
			.FirstOrDefaultAsync(x=>x.Guid == id), out var request) 
			? NotFound()
			: Ok(new InternRequestDto(request));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<InternRequestDto>>> Get(string title = null, Guid? organizationId = null, Guid? internshipDirectionId = null, string? tasks = null,
			int page = 1, int pageSize = 10, EntityType[] types = null)
		 => Ok( _dbContext.InternRequests
			 .WhereNotNull(title, x=> x.Name == title)
			 .WhereNotNull(organizationId, x=> x.OrganizationId == organizationId)
			 .WhereNotNull(internshipDirectionId, x => x.DirectionId == internshipDirectionId)
			 .WhereNotNull(tasks, x => x.TasksDescription.Contains(tasks))
			.FilterByUser(Identity)
			 .TakePage(page, pageSize)
			 .Select(x=> new InternRequestDto(x))
			 );

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Authorize(Roles =$"{nameof(UserType.Mentor)},{nameof(UserType.OrganizationAdmin)},{nameof(UserType.Admin)}")]
		public async Task<ActionResult<InternRequest>> Post(InternRequestDto request)
		{
			var req = _mapper.Map<InternRequest>(request);
			if (Identity.Role == UserType.OrganizationAdmin)
				req.OrganizationId = new Guid(Identity.OrganizationId);
			if (Identity.Role == UserType.Mentor)
				req.DirectionId = new Guid(Identity.DirectionId);
			_dbContext.Add(req);
			await _dbContext.SaveChangesAsync();
			return Ok(req);
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Roles =$"{nameof(UserType.Mentor)},{nameof(UserType.OrganizationAdmin)},{nameof(UserType.Admin)}")]
		public async Task<ActionResult> Put(InternRequestDto request)
		{
			var reqDb = _dbContext.InternRequests
				.AsNoTracking()
				.FilterByUser(Identity)
				.FirstOrDefault(x => x.Guid == request.Guid);
			if (reqDb == null)
				return NotFound();
			var req = _mapper.Map<InternRequest>(request);
			req.Id = reqDb.Id;
			_dbContext.Update(req);
			return Ok();
		}

	}
}
