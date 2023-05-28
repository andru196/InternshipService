using DataModel.Models;

namespace InternshipService.DTO
{
	public class CourseDto : NamedEntityDto
	{
		public string Link { get; set; }
		public CourseDto(Course course) : base(course)
		{
			Link = course.Link;
		}
	}
}
