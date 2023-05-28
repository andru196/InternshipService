using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using InternshipService.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewsController : ControllerBasePlusAuth
	{
		public ReviewsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview(Guid id) => Ok(new ReviewDto(
			_dbContext.Reviews.First(x => x.Guid == id)
			));

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(Guid? from, Guid? to, ReviewFor[] types = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.Reviews
			.WhereNotNull(to, x => x.To== to)
			.WhereNotNull(from, x => x.From == from)
			.WhereNotNull(types, x => types.Contains(x.ToEntityType))
			.TakePage(page, pageSize)
			.Select(x => new ReviewDto(x))
			);

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> PutReviews(ReviewDto reviewDto)
		{
			var reviewDb = await _dbContext.Reviews
				.FirstOrDefaultAsync(x => x.Guid == reviewDto.Id);
			if (reviewDb == null)
				return NotFound();
			if (reviewDto.From != Identity.Id && Identity.Role != UserType.Admin)
				return Unauthorized();
			var review = _mapper.Map<InternResponse>(reviewDto);
			review.Id = reviewDb.Id;
			_dbContext.Update(review);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
