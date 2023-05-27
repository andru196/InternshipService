using DataModel.Models;

namespace InternshipService.DTO
{
	public class BuddyDto
	{
		public Guid Id { get; set; }
		public FileRecordDto Avatar { get; set; }
		public double PartOfLikes { get; set; } = 0;
		public OrganizationDto Organization { get; set; }

		public BuddyDto(Buddy buddy, EntityType[] types = null)
		{
			types = types ?? new EntityType[0];
			Id = buddy.Guid;
			PartOfLikes = buddy.PartOfLikes;

			if (types.Contains(EntityType.File) && buddy.Avatar != null)
				Avatar = new FileRecordDto(buddy.Avatar);
			if (types.Contains(EntityType.Organization) && buddy.Organization != null)
				Organization = new OrganizationDto(buddy.Organization);
		}
	}
}
