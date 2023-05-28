using DataModel.Models;

namespace InternshipService.DTO
{
	public class OrganizationDto : NamedEntityDto
	{
		public string Address { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		public IEnumerable<OrganizationAdminDto> Admins { get; set; }
		public FileRecordDto? Avatar { get; set; }

		public OrganizationDto() : base() { }
		public OrganizationDto(Organization organization, EntityType[] types = null)
			: base(organization)
		{
			Address = organization.Address;
			Email = organization.Email;
			Phone = organization.Phone;

			if (types.Contains(EntityType.OrganizationAdmin))
				Admins = organization.Admins?.Select(x=> new OrganizationAdminDto(x))?.ToList() ?? Enumerable.Empty<OrganizationAdminDto>();
			if (types.Contains(EntityType.File) && organization.Avatar != null)
				Avatar = new FileRecordDto(organization.Avatar);
		}
	}
}
