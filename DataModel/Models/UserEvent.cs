namespace DataModel.Models
{
	public record UserEvent : Entity
	{
		public Guid UserId { get; set; }
		public Guid EventId { get; set; }
		public virtual User? User { get; set; }
		public virtual Event? Event { get; set; }
		public UserEventAttendStatus AttendStatus { get; set; }
	}
}
