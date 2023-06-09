﻿using AutoMapper;
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
	public class IntrernResponseController : ControllerBasePlusAuth
	{
		// TODO: рассылка notofications (email)

		// getMyStatus
		public IntrernResponseController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternResponseDto>> Get(Guid id, EntityType[] types = null) => Utils.IsNull(
			await _dbContext.InternResponses
			.Include(x => x.Intern)
			.Include(x => x.CV)
			.FilterByUser(Identity)
			.FirstOrDefaultAsync(x => x.Guid == id), out var response)
			? NotFound()
			: Ok(new InternResponseDto(response, types));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Roles =$"{nameof(UserType.Mentor)},{nameof(UserType.OrganizationAdmin)},{nameof(UserType.Admin)},{nameof(UserType.Intern)}")]
		public async Task<ActionResult<IEnumerable<InternResponseDto>>> Get(Guid? internId, string? text = null, int page = 1, int pageSize = 10, EntityType[] types = null) =>
			Ok(_dbContext.InternResponses
			.Include(x => x.Intern)
			.Include(x => x.CV)
			.FilterByUser(Identity)
			.AsNoTracking()
			.WhereNotNull(internId, x => x.InternId == internId)
			.WhereNotNull(text, x => x.Message.Contains(text))
			.TakePage(page, pageSize));

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Authorize(Roles =$"{nameof(UserType.Admin)},{nameof(UserType.Intern)}")]
		public async Task<ActionResult<InternResponseDto>> Post(InternResponseDto responseDto)
		{
			var response = _mapper.Map<InternResponse>(responseDto);
			if (Identity.Role == UserType.Intern)
				response.InternId = new Guid(Identity.UserId);
			response.Guid = Guid.NewGuid();
			_dbContext.Add(response);
			await _dbContext.SaveChangesAsync();
			return Ok(response);
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize(Roles = $"{nameof(UserType.Mentor)},{nameof(UserType.OrganizationAdmin)},{nameof(UserType.Admin)},{nameof(UserType.Intern)}")]
		public async Task<ActionResult> Put(InternResponseDto responseDto)
		{
			var resDb = await _dbContext.InternResponses
				.FilterByUser(Identity)
				.FirstOrDefaultAsync(x => x.Guid == responseDto.Guid);
			if (resDb == null)
				return NotFound();
			var response = _mapper.Map<InternResponse>(responseDto);
			response.Id = resDb.Id;
			if (Identity.Role == UserType.Intern)
			{
				response.InternId = resDb.InternId;
				response.Status = resDb.Status;
			}
			_dbContext.Update(response);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		private async Task RelevantInterns()
		{

		}
	}
}
