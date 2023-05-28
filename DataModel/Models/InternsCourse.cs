namespace DataModel.Models
{
	public record InternsCourse : Entity
	{
		public Guid InterId { get; set; }
		public Guid CourseId { get; set; }

		public virtual Intern? Intern { get; set; }
		public virtual Course? Course { get; set; }
		public bool IsViewed { get; set; }
	}
}
