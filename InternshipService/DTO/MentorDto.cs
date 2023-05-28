using DataModel.Models;

namespace InternshipService.DTO
{
	public class MentorDto : EntityDto
	{
		public virtual UserDto? User { get; set; }
		public Guid UserId { get; set; }
		public InternshipDirectionDto Direction { get; set; }
		public Guid DirectionId { get; set; }

		public MentorDto() : base() { }
		public MentorDto(Mentor mentor, EntityType[] types = null) : base(mentor)
		{
			types ??= new EntityType[0];
			UserId = mentor.UserId;
			DirectionId = mentor.DirectionId;
			if (types.Contains(EntityType.User))
				User = new UserDto(mentor.User);
			if (types.Contains(EntityType.InternshipDirection))
				Direction = new InternshipDirectionDto(mentor.Direction);
		}

	}
}
