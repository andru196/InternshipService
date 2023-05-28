namespace DataModel.Models
{
	public record Link : NamedEntity
	{
		public Guid ForId { get; set; }
		public EntityType EntityType { get; set; }
	}
}
