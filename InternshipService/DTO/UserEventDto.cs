using DataModel.Models;

namespace InternshipService.DTO
{
	public class UserEventDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid EventId { get; set; }
		public UserDto? User { get; set; }
		public EventDto? Event { get; set; }
		public UserEventAttendStatus AttendStatus { get; set; }

		public UserEventDto(UserEvent userEvent, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = userEvent.Guid;
			UserId = userEvent.UserId;
			EventId = userEvent.EventId;
			AttendStatus = userEvent.AttendStatus;
			if (types.Contains(EntityType.Event) && userEvent.Event != null)
				Event = new EventDto(userEvent.Event);
			if (types.Contains(EntityType.User) && userEvent.User != null)
				User = new UserDto(userEvent.User);
		}
	}
}
