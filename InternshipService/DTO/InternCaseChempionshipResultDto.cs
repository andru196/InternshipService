using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternCaseChempionshipResultDto : EntityDto
	{
		public Guid InternId { get; set; }
		public Guid CaseChempionshipId { get; set; }

		public InternDto? Intern { get; set; }
		public CaseChempionshipDto? CaseChempionship { get; set; }
		public bool IsViewed { get; set; }
		public uint Raiting { get; set; }

		public InternCaseChempionshipResultDto() : base() { }
		public InternCaseChempionshipResultDto(InternCaseChempionshipResult result, EntityType[] types = null)
			: base(result)
		{
			types = types ?? new EntityType[0];
			InternId = result.InternId;
			CaseChempionshipId = result.CaseChempionshipId;
			IsViewed = result.IsViewed;
			Raiting = result.Raiting;

			if (types.Contains(EntityType.Intern) && result.Intern != null)
				Intern = new InternDto(result.Intern, types);
			if (types.Contains(EntityType.CaseChempionship) && result.CaseChempionship != null)
				CaseChempionship = new CaseChempionshipDto(result.CaseChempionship);
		}
	}
}
