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
	public class NotificationsController : ControllerBasePlusAuth
	{
		public NotificationsController(ILogger logger, InternshipsDbContect context, IMapper mapper) : base(logger, context, mapper)
		{ }

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<NotificationDto>> Get(Guid id) => throw new NotImplementedException();

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<NotificationDto>>> Get(Guid userId, bool isNew = true, int page = 1, int pageSize = 20)
			=> throw new NotImplementedException();


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NotificationDto>> Post(NotificationDto notificationDto)
		{
			var notification = await PostAsync<Notification, NotificationDto>(notificationDto);
			return Ok(new NotificationDto(notification));
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Put(NotificationDto notificationDto)
		=> throw new NotImplementedException();


		[HttpGet("my")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetMy()
			=> Ok(_dbContext.Notifications.Where(x => x.To == Identity.Id).Select(x => new NotificationDto(x)));
	}
}
