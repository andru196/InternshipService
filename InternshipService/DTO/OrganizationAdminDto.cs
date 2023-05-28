using DataModel.Models;

namespace InternshipService.DTO
{
	public class OrganizationAdminDto : EntityDto
	{
		public Guid UserId { get; set; }
		public Guid OrganizationId { get; set; }
		public OrganizationDto Organization { get; set; }
		public UserDto User { get; set; }

		public OrganizationAdminDto() : base() { }
		public OrganizationAdminDto(OrganizationAdmin admin, EntityType[] types = null): base(admin)
		{
			types ??= new EntityType[0];
			UserId = admin.UserId;
			OrganizationId = admin.OrganizationId;
			if (types.Contains(EntityType.Organization) && admin.Organization != null)
				Organization = new OrganizationDto(admin.Organization, types);
			if (types.Contains(EntityType.User) && admin.User != null)
				User = new UserDto(admin.User);
		}
	}
}
