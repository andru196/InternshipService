namespace DataModel.Models
{
	public record UserTraining : NamedEntity
	{
		public Guid UserId { get; set; }
		public string Description { get; set; }
		public string Organization { get; set; }
		public ushort Hours { get; set; }
		public virtual User? User { get; set; }
		public virtual FileRecord? Certificate { get; set; }
		public Guid? CertificateId { get; set; }
	}
}
