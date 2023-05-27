namespace DataModel.Models
{
	public record InternResponse : Entity
	{
		public string Message { get; set; }
		public Guid InternId { get; set; }
		public virtual Intern? Intern { get; set; }
		public virtual FileRecord? CV { get; set; }
		public Guid? CVGuid { get; set; }
	}
}
