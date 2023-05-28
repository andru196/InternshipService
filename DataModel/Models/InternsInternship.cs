namespace DataModel.Models
{
	public record InternsInternship : Entity
	{
		public Guid PartOfInternResponseId { get; set; }
		public Guid PartOfInternRequestId { get; set; }
		public DateOnly BeginDate { get; set; }
		public DateOnly EndDate { get; set; }
		public RequsetResponseStatus Status { get; set; }
	}
}
