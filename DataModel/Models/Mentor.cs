namespace DataModel.Models
{
	public record Mentor : Entity
	{
		public virtual User? User { get; set; }
		public Guid UserId { get; set; }
		public virtual InternshipDirection Direction { get; set; }
		public Guid DirectionId { get; set; }
	}
}
