using DataModel.Models;

namespace InternshipService.DTO
{
	public class MentorDto
	{
		public Guid Id { get; set; }
		public InternshipDirectionDto Direction { get; set; }

		public MentorDto(Mentor mentor, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = mentor.Guid;
			if (types.Contains(EntityType.Mentor))
				Direction = new InternshipDirectionDto(mentor.Direction);
		}

	}
}
