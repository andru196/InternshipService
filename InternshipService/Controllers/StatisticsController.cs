using AutoMapper;
using DataModel.Context;
using InternshipService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatisticsController : ControllerBasePlusAuth
	{
		public StatisticsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{ }


		public record StatisticDto(double RelevanExperiance,
			Dictionary<string, double> FederalDistrict,
			Dictionary<string, double> Adge,
			Dictionary<string, double> University,
			Dictionary<string, double> Degree,
			Dictionary<string, double> Channel);

		[HttpGet()]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Get()
		{
			var thisYear = _dbContext.InternResponses.Where(x => x.Year == DateTime.Now.Year);
			var count = (double)thisYear.Count();

			return Ok(new StatisticDto(
				RelevanExperiance: thisYear.Count(x => x.HaveNeededExperience) / count,
				FederalDistrict: thisYear.GroupBy(x => x.FederalDistrict).ToDictionary(x => x.Key, x => x.Count() / count),
				Adge: thisYear.GroupBy(x => x.Intern.BirthDate.Year).ToDictionary(x => x.Key.ToString(), x => x.Count() / count),
				University: thisYear.GroupBy(x => x.Intern.University.Name).ToDictionary(x => x.Key, x => x.Count() / count),
				Degree: thisYear.GroupBy(x => x.Education.ToString()).ToDictionary(x => x.Key, x => x.Count() / count),
				Channel: thisYear.GroupBy(x => _dbContext.Arrivals.FirstOrDefault(a => a.UserId == x.Intern.UserId).Channel).ToDictionary(x => x.Key, x => x.Count() / count)
				)
			);
		}
	}
}
