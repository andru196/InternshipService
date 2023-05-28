using DataModel.Models;

namespace InternshipService.DTO
{
	public class BuddyInternLinkDto : EntityDto
	{
		public Guid InternInternshipId { get; set; }
		public Guid BuddyId { get; set; }
		public BuddyDto Buddy { get; set; }
		public InternsInternshipDto Intern { get; set; }

		public BuddyInternLinkDto(BuddyInternLink entity, EntityType[] types = null) : base(entity)
		{
			types ??= new EntityType[0];

			InternInternshipId = entity.InternInternshipId;
			BuddyId = entity.BuddyId;

			if (types.Contains(EntityType.Buddy) && entity.Buddy != null)
				Buddy = new BuddyDto(entity.Buddy);
			if (types.Contains(EntityType.InternsInternship) && entity.Intern != null)
				Intern = new InternsInternshipDto(entity.Intern);
		}
	}
}
