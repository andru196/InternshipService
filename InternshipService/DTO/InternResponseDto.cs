using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternResponseDto : EntityDto
	{
		public Guid InternId { get; set; }
		public string Message { get; set; }
		public Guid? CVId { get; set; }
		public EducationDegree Education { get; set; }
		public ushort Course { get; set; }
		public DateTime? CreatedDate { get; set; } = DateTime.Now;
		public InternResponseStatus Status { get; set; }
		public Guid SelectedInternship1ID { get; set; }
		public Guid SelectedInternship2ID { get; set; }
		public Guid SelectedInternship3ID { get; set; }
		public string FederalDistrict { get; set; }

		public FileRecordDto? CV { get; set; }
		public InternDto Intern { get; set; }

		public InternResponseDto() : base() { }
		public InternResponseDto(InternResponse response, EntityType[] types = null)
			: base(response)
		{
			types ??= new EntityType[0];
			Message = response.Message;
			InternId = response.InternId;
			Education = response.Education;
			Course = response.Course;
			Status = response.Status;
			CreatedDate = response.CreatedDate;
			SelectedInternship1ID = response.SelectedInternship1ID;
			SelectedInternship2ID = response.SelectedInternship2ID;
			SelectedInternship3ID = response.SelectedInternship3ID;
			CVId = response.CVId;
			FederalDistrict = response.FederalDistrict;
			if (types.Contains(EntityType.File) && response.CV != null)
				CV = new FileRecordDto(response.CV);
			if (types.Contains(EntityType.Intern) && response.Intern != null)
				Intern = new InternDto(response.Intern, types);
		}
	}
}
