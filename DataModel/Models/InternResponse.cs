namespace DataModel.Models
{
	public record InternResponse : Entity
	{
		public string Message { get; set; }
		public Guid InternId { get; set; }
		public virtual Intern? Intern { get; set; }
		public Guid InternRequestId { get; set; }
		public virtual InternRequest? InternRequest { get; set; }
		public virtual FileRecord? CV { get; set; }
		public string EducationSpecialiazation { get; set; }
		public EducationDegree Education { get; set; }
		public ushort Course { get; set; }
		public Guid? CVGuid { get; set; }
		public InternResponseStatus Status { get; set; } = InternResponseStatus.New;
	}
}
