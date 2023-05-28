using DataModel.Models;

namespace InternshipService.DTO
{
	public class CaseChempionshipDto : NamedEntityDto
	{
		public string Link { get; set; }
		public CaseChempionshipDto(CaseChempionship caseChempionship) : base(caseChempionship)
		{
			Link = caseChempionship.Link;
		}
	}
}
