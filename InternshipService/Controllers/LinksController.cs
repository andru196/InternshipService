using AutoMapper;
using DataModel.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LinksController : ControllerBasePlusAuth
	{
		public LinksController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{
		}

		// TODO: добавить ссылку на карьерную школу
	}
}
