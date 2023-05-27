namespace DataModel.Models
{
	public record Event : NamedEntity
	{
		public string Description { get; set; }
		public string Address { get; set; }
		public Guid? ByOrganizationId { get; set; }
		public virtual Organization? ByOrganization { get; set; }
		public string? ByOrganizationName { get; set; }
		public DateTime Begin { get; set; }
		public DateTime? End { get; set; }
	}
}
