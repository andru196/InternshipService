using DataModel.Models;

namespace InternshipService.DTO
{
	public class OrganizationDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public IEnumerable<OrganizationAdminDto> Admins { get; set; }
		public FileRecordDto? Avatar { get; set; }

		public OrganizationDto(Organization organization, EntityType[] types = null)
		{
			Id = organization.Guid;
			Name = organization.Name;
			Address = organization.Address;
			Email = organization.Email;
			if (types.Contains(EntityType.OrganizationAdmin))
				Admins = organization.Admins?.Select(x=> new OrganizationAdminDto(x))?.ToList() ?? Enumerable.Empty<OrganizationAdminDto>();
			if (types.Contains(EntityType.File) && organization.Avatar != null)
				Avatar = new FileRecordDto(organization.Avatar);
		}
	}
}
