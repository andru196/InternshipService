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
	public class TestsController : ControllerBasePlusAuth
	{
		public TestsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper) {}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<TestDto>> Get(Guid id) => throw new NotImplementedException();


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<TestDto>>> Get(Guid? organizationId = null, string? name = null, int page = 1, int pageSize = 10) => Ok(_dbContext.Tests
			.WhereNotNull(organizationId, x => x.OrganizationId == organizationId)
			.WhereNotNull(name, x => x.Name.Contains(name))
			.TakePage(page, pageSize));

		
		[Authorize($"{nameof(UserType.Admin)},{nameof(UserType.OrganizationAdmin)}")]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<TestDto>> Post(TestDto testDto)
		{
			var test = await PostAsync<Test, TestDto>(testDto);
			return (Ok(new TestDto(test)));
		}

		[Authorize($"{nameof(UserType.Admin)},{nameof(UserType.OrganizationAdmin)}")]
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(TestDto testDto)
		{
			var testDb = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Guid == testDto.Guid);
			if (testDb == null) return NotFound();
			var test = _mapper.Map<Test>(testDto);
			test.Id = testDb.Id;
			_dbContext.Tests.Update(test);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}


		[HttpGet("interns/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternTestDto>> GetInternsTest(Guid id) => Utils.IsNull(await _dbContext.InternTest.FirstOrDefaultAsync(x=>x.Guid==id), out var test)
			? NotFound()
			: Ok(new InternTestDto(test));


		[HttpGet("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<InternTestDto>>> GetInternsTests(Guid? internId = null, int page = 1, int pageSize = 10) => Ok(
			_dbContext.InternTest.WhereNotNull(internId, x => x.InterId == internId)
			.TakePage(page, pageSize)
			.Select(x=> new InternTestDto(x))
			);

		[HttpPost("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<InternTestDto>> PostInternsTest(InternTestDto testDto)
		{
			var test = await PostAsync<InternTest, InternTestDto>(testDto);
			return (Ok(new InternTestDto(test)));
		}


		[HttpPut("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> PutInternsTest(InternTestDto testDto)
		{
			var testDb = await _dbContext.InternTest.FirstOrDefaultAsync(x => x.Guid == testDto.Guid);
			if (testDb == null) return NotFound();
			var test = _mapper.Map<InternTest>(testDto);
			test.Id = testDb.Id;
			_dbContext.InternTest.Update(test);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
