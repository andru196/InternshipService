namespace DataModel.Models
{
	public record InternResponse : Entity
	{
		public string Message { get; set; }
		public bool HaveNeededExperience { get; set; }
		public Guid InternId { get; set; }
		public string FederalDistrict { get; set; }
		public virtual Intern? Intern { get; set; }
		public virtual FileRecord? CV { get; set; }
		public string EducationSpecialiazation { get; set; }
		public EducationDegree Education { get; set; }
		public ushort Course { get; set; }
		public Guid? CVId { get; set; }
		public InternResponseStatus Status { get; set; } = InternResponseStatus.New;
		public DateTime CreatedDate { get; set; }
		public short Year { get; set; }
		public Guid SelectedInternship1ID { get; set; }
		public Guid SelectedInternship2ID { get; set; }
		public Guid SelectedInternship3ID { get; set; }
	}
}
