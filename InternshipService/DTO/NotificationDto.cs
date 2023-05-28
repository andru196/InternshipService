using DataModel.Models;

namespace InternshipService.DTO
{
	public class NotificationDto : NamedEntityDto
	{
		public string Body { get; set; }
		public Guid From { get; set; }
		public Guid To { get; set; }
		public bool Viewed { get; set; }

		public NotificationDto(Notification entity) : base(entity)
		{
			Body = entity.Body;
			From = entity.From;
			To = entity.To;
			Viewed = entity.Viewed;
		}
	}
}
