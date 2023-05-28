using DataModel.Models;

namespace InternshipService.DTO
{
	public class TagDto : NamedEntityDto
	{

		public TagDto() : base() { }
		public TagDto(Tag tag) : base(tag) { }
	}
}
