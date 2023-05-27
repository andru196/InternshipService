namespace InternshipService.DTO
{
	public class NotificationDto
	{
		public string Body { get; set; }
		public Guid From { get; set; }
		public Guid To { get; set; }
		public bool Viewed { get; set; }
	}
}
