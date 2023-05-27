using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime BirthDate { get; set; }
		public string About { get; set; }
		public Guid? AvatarId { get; set; }
		public Guid? UnivercityId { get; set; }
		public short StartOfEducation { get; set; }
		public InternStatus Status { get; set; }
		public IEnumerable<TagDto> Tags { get; set; }
		public UniversityDto Univercity { get; set; }
		public FileRecordDto Avatar { get; set; }
		public IEnumerable<UserEventDto> Events { get; set; }

		public InternDto(Intern intern, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = intern.Guid;
			UserId = intern.UserId;
			BirthDate = intern.BirthDate;
			About = intern.About;
			AvatarId = intern.AvatarId;
			UnivercityId = intern.UniversityId;
			StartOfEducation = intern.StartOfEducation;
			Status = intern.Status;
			Tags = intern.Tags?.Select(x=> new TagDto(x))?.ToList() ?? Enumerable.Empty<TagDto>();
			if (types.Contains(EntityType.University) && intern.University != null)
				Univercity = new UniversityDto(intern.University);
			if (types.Contains(EntityType.File) && intern.Avatar != null)
				Avatar = new FileRecordDto(intern.Avatar);
			if (types.Contains(EntityType.Event))
				Events = intern.Events?.Select(x=> new UserEventDto(x))?.ToList() ?? Enumerable.Empty<UserEventDto>();
		}
	}
}
