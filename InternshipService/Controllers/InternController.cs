using AutoMapper;
using DataModel.Context;
using DataModel.Models;
using InternshipService.Configs;
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
		private InternAutoCheckConfig _autoCheckConfig;
		public InternController(ILogger logger, InternshipsDbContect context, IMapper mapper, InternAutoCheckConfig autoCheckConfig) : base(logger, context, mapper) => _autoCheckConfig = autoCheckConfig;


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

		[Authorize(Roles = nameof(UserType.Student))]
		[HttpGet("MyStatus")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<InternResponseStatus>> GetMyStatus()
		=> Ok((await _dbContext.InternResponses
			.Where(x=>x.Intern.User.Guid == Identity.Id)
			.FirstOrDefaultAsync(x=> x.Year == DateTime.Now.Year))
			?.Status ?? InternResponseStatus.New);

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

		/// <summary>
		/// Иницирует автоматическую проверку заявок по заданым параметрам 
		/// </summary>
		/// <returns>Список отвергнутых заявок</returns>
		[Authorize(Roles = $"{nameof(UserType.Mentor)},{nameof(UserType.Admin)}")]
		[HttpGet("AutoCheck")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<InternResponseDto>>> AutoCheck()
		{
			var rejectedResponse = _dbContext.InternResponses
				.Include(x=>x.Intern)
				.ThenInclude(x=>x.User)
				.Where(x => 
					((x.Education == EducationDegree.Bachelor || x.Education == EducationDegree.Specialist) && x.Course < _autoCheckConfig.MinimalCourse)
					|| (DateTime.Now.Year - x.Intern.BirthDate.Year) < _autoCheckConfig.Age.From || (DateTime.Now.Year - x.Intern.BirthDate.Year) > _autoCheckConfig.Age.To
					|| !_autoCheckConfig.Citizen.Contains(x.Intern.Citizenship)
					|| (_autoCheckConfig.NeedRelevantExperiance && !x.HaveNeededExperience));
			await rejectedResponse.ForEachAsync(x => { 
				x.Status = InternResponseStatus.Rejected;
				_dbContext.Notifications.Add(new Notification
				{
					Body = string.Format(_autoCheckConfig.SorryMsg, x.Intern.User.SecondName, x.Intern.User.FirstName, x.Intern.User.MiddleName),
					From = _autoCheckConfig.FromUser,
					Guid = Guid.NewGuid(),
					Name = string.Format(_autoCheckConfig.SorryMsgSubject, x.Intern.User.SecondName, x.Intern.User.FirstName, x.Intern.User.MiddleName),
					To = x.Intern.Guid
				});
			}
			);
			await _dbContext.SaveChangesAsync();
			return Ok(rejectedResponse.Select(x=> new InternResponseDto(x, null)));
		}
	}
}
