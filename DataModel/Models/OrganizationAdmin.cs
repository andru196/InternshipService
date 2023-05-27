namespace DataModel.Models
{
	public record OrganizationAdmin : Entity
	{
		public Guid UserId { get; set; }
		public Guid OrganizationId { get; set; }
		public virtual Organization? Organization { get; set; }
		public virtual User? User{ get; set; }
	}
}
