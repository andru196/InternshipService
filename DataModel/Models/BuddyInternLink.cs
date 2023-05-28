namespace DataModel.Models
{
	public record BuddyInternLink : Entity
	{
		public Guid InternInternshipId { get; set; }
		public Guid BuddyId { get; set; }
		public virtual Buddy Buddy { get; set; }
		public virtual InternsInternship Intern { get; set; }
	}
}
