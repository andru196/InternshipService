using DataModel.Models;

namespace InternshipService.DTO
{
	public class UserEventDto : EntityDto
	{
		public Guid UserId { get; set; }
		public Guid EventId { get; set; }
		public UserDto? User { get; set; }
		public EventDto? Event { get; set; }
		public UserEventAttendStatus AttendStatus { get; set; }

		public UserEventDto(UserEvent userEvent, EntityType[] types = null)
			: base(userEvent)
		{
			types ??= new EntityType[0];
			UserId = userEvent.UserId;
			EventId = userEvent.EventId;
			AttendStatus = userEvent.AttendStatus;
			if (types.Contains(EntityType.Event) && userEvent.Event != null)
				Event = new EventDto(userEvent.Event, types);
			if (types.Contains(EntityType.User) && userEvent.User != null)
				User = new UserDto(userEvent.User);
		}
	}
}
