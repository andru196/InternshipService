namespace DataModel.Models
{
	public record Organization : NamedEntity
	{
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public virtual IEnumerable<OrganizationAdmin> Admins { get; set; }
		public virtual FileRecord? Avatar { get; set; }
	}
}
