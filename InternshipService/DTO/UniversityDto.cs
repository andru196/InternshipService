using DataModel.Models;

namespace InternshipService.DTO
{
	public class UniversityDto : NamedEntityDto
	{
		public string City { get; set; }
		public string Address { get; set; }
		public Guid? AvatarId { get; set; }
		public FileRecordDto? Avatar { get; set; }

		public UniversityDto(University university, EntityType[] types = null)
			: base(university)
		{
			types ??= new EntityType[0];
			City = university.City;
			Address = university.Address;
			AvatarId = university.AvatarId;

			if (types.Contains(EntityType.File) && university.Avatar != null)
				Avatar = new FileRecordDto(university.Avatar);
		}
	}
}
