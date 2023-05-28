namespace DataModel.Models
{
	public record InternRequest: NamedEntity
	{
		public string Description { get; set; }
		public string Tribe { get; set; }
		public string TasksDescription { get; set; }
		public string Email { get; set; }
		public string? Address { get; set; }
		public Guid CreatedByGuid { get; set; }
		public virtual OrganizationAdmin CreatedBy { get; set; }
		public Guid OrganizationId { get; set; }
		public virtual Organization Organization { get; set; }
		public virtual IEnumerable<Link>? Links { get; set; }
		public Guid DirectionId {  get; set; }
		public virtual InternshipDirection Direction {  get; set; }
		public double Latitude {  get; set; }
		public double Longitude {  get; set; }
		public InternRequestStatus Status { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
