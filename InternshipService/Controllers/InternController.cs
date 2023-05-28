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
	public class InternController : ControllerBasePlusAuth
	{
		public InternController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) { }


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternDto>> Get(Guid id) => Utils.IsNull(_dbContext.Interns
			.Include(x => x.User)
			.Include(x => x.Avatar)
			.Include(x => x.University)
			.Include(x => x.Tags)
			.Include(x => x.Events)
			.AsNoTracking()
			.FirstOrDefault(x => x.Guid == id), out var intern) ?
			Ok(new InternDto(intern)) : NotFound();

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<InternDto>>> Get(string name = null, Guid? universityId = null, int page = 1,
			InternStatus? status = null, Guid? eventId = null, int pageSize = 10, EntityType[] types = null)
		=> Ok(_dbContext.Interns
			.Include(x => x.User)
			.Include(x => x.Events)
			.Include(x=>x.Tags)
				.WhereNotNull(name, x=> $"{x.User.SecondName} {x.User.FirstName} {x.User.MiddleName}".Contains(name))
				.WhereNotNull(universityId, x => x.UniversityId == universityId)
				.WhereNotNull(status, x=> x.Status == status)
				.WhereNotNull(eventId, x => x.Events.Any(y=>y.EventId == eventId))
				.TakePage(page, pageSize)
			.Select(x=>new InternDto(x, types)));

		[Authorize(Roles =$"{nameof(UserType.None)},{nameof(UserType.Admin)}")]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<InternDto>> Post(InternDto internDto)
		{
			var intern = _mapper.Map<Intern>(internDto);
			if (Identity.Role == UserType.None)
				intern.UserId = new Guid(Identity.UserId);
			intern.Guid = internDto.UserId;
			_dbContext.Add(intern);
			await _dbContext.SaveChangesAsync();
			return Ok(new InternDto(intern));
		}

		[Authorize(Roles =$"{nameof(UserType.Student)},{nameof(UserType.Admin)}")]
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(InternDto internDto)
		{
			var internDb = _dbContext.Interns.Where(x=>Identity.Role == UserType.Admin || x.UserId == new Guid(Identity.UserId))
				.FirstOrDefault(x=>internDto.Id == x.Guid)
				?? throw new HttpException(System.Net.HttpStatusCode.NotFound);
			var intern = _mapper.Map<Intern>(internDto);
			intern.Id = internDb.Id;
			intern.UserId = internDb.UserId;
			_dbContext.Update(intern);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("reviews")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ReviewDto>> PostReview(ReviewDto reviewDto)
		{
			var review = _mapper.Map<Review>(reviewDto);
			review.From = new Guid(Identity.UserId);
			_dbContext.Add(review);
			await _dbContext.SaveChangesAsync();
			return Ok(new ReviewDto(review));
		}


		[HttpGet("reviews/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(Guid id) => Ok(new ReviewDto(
			_dbContext.InternReviews.First(x => x.Guid == id)
			));

		[HttpGet("reviews")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(Guid? from, Guid? to, int page = 1, int pageSize = 10) => Ok(
			_dbContext.InternReviews
			.WhereNotNull(to, x => x.InternId == to)
			.WhereNotNull(from, x => x.From == from)
			.TakePage(page, pageSize)
			.Select(x => new InternReviewDto(x))
			);

		[HttpPut("reviews")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> PutReviews(ReviewDto reviewDto)
		{
			var reviewDb = await _dbContext.InternReviews.Where(x => Identity.Role == UserType.Admin || x.From == new Guid(Identity.UserId)).FirstOrDefaultAsync(x => x.Guid == reviewDto.Id);
			if (reviewDb == null)
				return NotFound();
			var review = _mapper.Map<InternResponse>(reviewDto);
			review.Id = reviewDb.Id;
			_dbContext.Update(review);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

	}
}
