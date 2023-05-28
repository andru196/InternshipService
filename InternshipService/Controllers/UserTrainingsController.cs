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
	public class UserTrainingsController : ControllerBasePlusAuth
	{
		public UserTrainingsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserTrainingDto>> Get(Guid id) => Utils.IsNull(_dbContext.UserTrainings.FirstOrDefault(x=>x.Guid == id), out var trainings) ?
			NotFound() : Ok(trainings);


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<UserTrainingDto>>> Get(Guid? userId = null, int page = 1, int pageSize = 10) => Ok(_dbContext.UserTrainings.WhereNotNull(userId, x => x.UserId == userId).
			TakePage(page, pageSize));


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<UserTrainingDto>> Post(UserTrainingDto trainingDto)
		{
			var training = _mapper.Map<UserTraining>(trainingDto);
			_dbContext.Add(training);
			await _dbContext.SaveChangesAsync();
			return Ok(training);
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(UserTrainingDto trainingDto)
		{
			var trainingDb = await _dbContext.UserTrainings.FirstOrDefaultAsync(x => x.Guid == trainingDto.Guid);
			if (trainingDb?.UserId != Identity.Id && Identity.Role != UserType.Admin)
				return Forbid();
			var training = _mapper.Map<UserTraining>(trainingDto);
			training.UserId = trainingDto.UserId;
			_dbContext.UserTrainings.Update(training);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
