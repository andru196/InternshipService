using DataModel.Models;

namespace InternshipService.DTO
{
	public class BuddyInternLinkDto : EntityDto
	{
		public Guid InternInternshipId { get; set; }
		public Guid BuddyId { get; set; }
		public BuddyDto Buddy { get; set; }
		public InternsInternshipDto Intern { get; set; }

		public BuddyInternLinkDto(BuddyInternLink entity) : base(entity)
		{
			InternInternshipId = entity.InternInternshipId;
			BuddyId = entity.BuddyId;
			// TODO: FINISH
		}
	}
}
