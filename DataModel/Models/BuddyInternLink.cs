namespace DataModel.Models
{
	public record BuddyInternLink : Entity
	{
		public Guid InternInternshipId { get; set; }
		public Guid BuddyId { get; set; }
		public Buddy Buddy { get; set; }
		public InternsInternship Intern { get; set; }
	}
}
