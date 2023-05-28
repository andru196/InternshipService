using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MentorsController : ControllerBasePlusAuth
	{
		public MentorsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<MentorDto>> Get(Guid id) => throw new NotImplementedException();


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<MentorDto>>> Get(Guid? directionId = null, int page = 1, int pageSize = 10) => throw new NotImplementedException();

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<MentorDto>> Post(MentorDto caseChempionshipDto)
		{
			var caseChempionship = await PostAsync<Mentor, MentorDto>(caseChempionshipDto);
			return Ok(new MentorDto(caseChempionship));
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<MentorDto>> Put(MentorDto caseChempionshipDto) => throw new NotImplementedException();

	}
}
