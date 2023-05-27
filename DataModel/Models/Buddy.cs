namespace DataModel.Models
{
	public record Buddy: Entity
	{
		public Guid UserId { get; set; }
		public virtual User? User { get; set; }
		public virtual FileRecord? Avatar { get; set; }
		public Guid OrganizationId { get; set; }
		public Guid? AvatarGuid { get; set; }
		public double PartOfLikes { get; set; } = 0;
		public virtual Organization Organization { get; set; }
	}
}
