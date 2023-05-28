using Azure;
using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternsCourseDto : EntityDto
	{
		public Guid InterId { get; set; }
		public Guid CourseId { get; set; }

		public InternDto? Intern { get; set; }
		public CourseDto? Course { get; set; }
		public bool IsViewed { get; set; }
		public InternsCourseDto() : base() { }
		public InternsCourseDto(InternsCourse course, EntityType[] types = null) 
			: base(course)
		{
			types ??= new EntityType[0];

			InterId = course.InterId;
			CourseId = course.CourseId;
			IsViewed = course.IsViewed;

			if (types.Contains(EntityType.Course) && course.Course != null)
				Course = new CourseDto(course.Course);
			if (types.Contains(EntityType.Intern) && course.Intern != null)
				Intern = new InternDto(course.Intern, types);
		}
	}
}
