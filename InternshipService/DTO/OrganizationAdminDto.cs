using DataModel.Models;

namespace InternshipService.DTO
{
	public class OrganizationAdminDto 
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid OrganizationId { get; set; }
		public OrganizationDto Organization { get; set; }
		public UserDto User { get; set; }

		public OrganizationAdminDto(OrganizationAdmin admin, EntityType[] types = null) {
			types ??= new EntityType[0];
			Id = admin.Guid;
			UserId = admin.UserId;
			OrganizationId = admin.OrganizationId;
			if (types.Contains(EntityType.Organization) && admin.Organization != null)
				Organization = new OrganizationDto(admin.Organization);
			if (types.Contains(EntityType.User) && admin.User != null)
				User = new UserDto(admin.User);
		}
	}
}
