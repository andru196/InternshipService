using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternsInternshipDto : EntityDto
	{
		public Guid PartOfInternResponseId { get; set; }
		public Guid PartOfInternRequestId { get; set; }
		public DateOnly BeginDate { get; set; }
		public DateOnly EndDate { get; set; }
		public RequsetResponseStatus Status { get; set; }

		public InternsInternshipDto() : base() { }
		public InternsInternshipDto(InternsInternship internship) : base(internship)
		{
			PartOfInternRequestId = internship.PartOfInternRequestId;
			PartOfInternResponseId = internship.PartOfInternResponseId;
			BeginDate = internship.BeginDate;
			EndDate = internship.EndDate;
			Status = internship.Status;
		}
	}
}
