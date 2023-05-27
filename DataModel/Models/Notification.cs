namespace DataModel.Models
{
	public record Notification : NamedEntity
	{
		public string Body { get; set; }
		public Guid From { get; set; }
		public Guid To { get; set; }
		public bool Viewed { get; set; }
	}
}
