namespace DataModel.Models
{
	public record University : NamedEntity
	{
		public string City { get; set; }
		public string Address { get; set; }
		public virtual FileRecord? Avatar { get; set; }
		public Guid? AvatarId { get; set; }
	}
}
