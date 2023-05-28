using DataModel.Models;

namespace InternshipService.DTO
{
	public class BuddyDto : EntityDto
	{
		public FileRecordDto Avatar { get; set; }
		public double Raiting { get; set; } = 0;
		public OrganizationDto Organization { get; set; }
		public UserDto? User { get; set; }

		public BuddyDto(Buddy buddy, EntityType[] types = null) : base(buddy)
		{
			types = types ?? new EntityType[0];
			Raiting = buddy.Raiting;

			if (types.Contains(EntityType.File) && buddy.Avatar != null)
				Avatar = new FileRecordDto(buddy.Avatar);
			if (types.Contains(EntityType.Organization) && buddy.Organization != null)
				Organization = new OrganizationDto(buddy.Organization);
			if (types.Contains(EntityType.User) && buddy.User != null)
				User = new UserDto(buddy.User);
		}
	}
}
