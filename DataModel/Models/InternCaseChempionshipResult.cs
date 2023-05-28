namespace DataModel.Models
{
	public record InternCaseChempionshipResult : Entity
	{
		public Guid InternId { get; set; }
		public Guid CaseChempionshipId { get; set; }

		public virtual Intern? Intern { get; set; }
		public virtual CaseChempionship? CaseChempionship { get; set; }
		public bool IsViewed { get; set; }
		public uint Raiting { get; set; }
	}
}
