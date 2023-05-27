using DataModel.Models;

namespace InternshipService.DTO
{
	public class UniversityDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public FileRecordDto? Avatar { get; set; }

		public UniversityDto(University university, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Name = university.Name;
			City = university.City;
			Address = university.Address;
			if (types.Contains(EntityType.File) && university.Avatar != null)
				Avatar = new FileRecordDto(university.Avatar);
		}
	}
}
