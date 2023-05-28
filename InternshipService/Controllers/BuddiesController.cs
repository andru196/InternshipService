using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuddiesController : ControllerBasePlusAuth
	{
		public BuddiesController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<BuddyDto>> Get(Guid id) => throw new NotImplementedException();
			

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<BuddyDto>>> Get(Guid? organizationId = null, int page = 1, int pageSize = 10) => throw new NotImplementedException();

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<BuddyDto>> Post(BuddyDto buddyDto) => throw new NotImplementedException();


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<TagDto>> Put(BuddyDto buddyDto) => throw new NotImplementedException();


		[HttpGet("interns/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<BuddyDto>> GetIntern(Guid id) => throw new NotImplementedException();


		[HttpGet("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<BuddyDto>>> GetIntern(Guid? organizationId = null, int page = 1, int pageSize = 10) => throw new NotImplementedException();

		[HttpPost("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<BuddyDto>> PostIntern(BuddyDto buddyDto) => throw new NotImplementedException();


		[HttpPut("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<TagDto>> PutIntern(BuddyDto buddyDto) => throw new NotImplementedException();
	}
}
