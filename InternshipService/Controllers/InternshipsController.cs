using AutoMapper;
using DataModel.Context;
using InternshipService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InternshipsController : ControllerBasePlusAuth
	{
		public InternshipsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}

		[HttpGet("intern/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternsInternshipDto>> Get(Guid id) => throw new NotImplementedException();


		[HttpGet("intern")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<InternsInternshipDto>>> Get(Guid? organizationId = null, int page = 1, int pageSize = 10) => throw new NotImplementedException();

		[HttpPost("intern")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<InternsInternshipDto>> Post(InternsInternshipDto buddyDto) => throw new NotImplementedException();


		[HttpPut("intern")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternsInternshipDto>> Put(InternsInternshipDto buddyDto) => throw new NotImplementedException();
	}
}
