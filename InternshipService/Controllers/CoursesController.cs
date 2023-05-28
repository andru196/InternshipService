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
	public class CoursesController : ControllerBasePlusAuth
	{
		public CoursesController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<CourseDto>> Get(Guid id) => throw new NotImplementedException();


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<CourseDto>>> Get(Guid? organizationId = null, int page = 1, int pageSize = 10) => throw new NotImplementedException();

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<CourseDto>> Post(CourseDto courseDto)
		{
			var course = await PostAsync<Course, CourseDto>(courseDto);
			return Ok(new CourseDto(course));
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<CourseDto>> Put(CourseDto tagDbo) => throw new NotImplementedException();




		[HttpGet("interns/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternsCourseDto>> GetInternsCourse(Guid id) => throw new NotImplementedException();


		[HttpGet("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<IEnumerable<InternsCourseDto>>> GetInternsCourses(Guid? interId = null, Guid? courseId = null, int page = 1, int pageSize = 10) => throw new NotImplementedException();
																						 
		[HttpPost("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<InternsCourseDto>> PostInternsCourse(InternsCourseDto courseDto)
		{
			var course = await PostAsync<InternsCourse, InternsCourseDto>(courseDto);
			return Ok(new InternsCourseDto(course));
		}


		[HttpPut("interns")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternsCourseDto>> PutInternsCourse(InternsCourseDto tagDbo) => throw new NotImplementedException();
	}
}
