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
	public class OrganizationController : ControllerBasePlusAuth
	{
		public OrganizationController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<OrganizationDto>> Get(Guid id, EntityType[] types = null) => Utils.IsNull(
			await _dbContext.Organizations
			.Include(x=> x.Avatar)
			.Include(x => x.Admins)
			.FirstOrDefaultAsync(x => x.Guid == id), out var organization)
			? NotFound()
			: Ok(new OrganizationDto(organization, types));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<OrganizationDto>>> Get(string name = null, int page = 1, int pageSize = 10, EntityType[] types = null) =>
			Ok(_dbContext.Organizations.AsNoTracking()
				.WhereNotNull(name, x=>x.Name.Contains(name))
				.TakePage(page, pageSize)
				.Select(x=> new OrganizationDto(x, types))
				);

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Authorize(Roles = $"{nameof(UserType.Mentor)},{nameof(UserType.Admin)}")]
		public async Task<ActionResult<OrganizationDto>> Post(OrganizationDto organiztionDto)
		{
			var organiztion = _mapper.Map<Organization>(organiztionDto);
			_dbContext.Add(organiztion);
			await _dbContext.SaveChangesAsync();
			return Ok(organiztion);
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Roles = $"{nameof(UserType.Mentor)},{nameof(UserType.OrganizationAdmin)},{nameof(UserType.Admin)}")]
		public async Task<ActionResult<OrganizationDto>> Put(OrganizationDto organizationDto)
		{
			var orgDb = await _dbContext.Organizations
				.WhereNotNull(Identity.Role == UserType.OrganizationAdmin, x=>x.Guid == new Guid(Identity.OrganizationId))
				.FirstOrDefaultAsync(x => x.Guid == organizationDto.Id);
			if (orgDb == null)
				return NotFound();
			var organization = _mapper.Map<InternResponse>(organizationDto);
			organization.Id = orgDb.Id;
			_dbContext.Update(organization);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}


		[HttpGet("Admins")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<OrganizationAdminDto>>> Get(Guid orgId, int page = 1, int pageSize = 10, EntityType[] types = null) =>
			Ok(_dbContext.OrganizationAdmins.AsNoTracking()
				.Where(x=>x.OrganizationId == orgId)
				.TakePage(page, pageSize)
				.Select(x => new OrganizationAdminDto(x, types))
				);
	}
}
