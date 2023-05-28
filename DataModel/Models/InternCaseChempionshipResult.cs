namespace DataModel.Models
{
	public record InternCaseChempionshipResult : Entity
	{
		public Guid InterId { get; set; }
		public Guid CaseChempionshipId { get; set; }

		public Intern? Inter { get; set; }
		public CaseChempionship? CaseChempionship { get; set; }
		public bool IsViewed { get; set; }
		public uint Raiting { get; set; }
	}
}
