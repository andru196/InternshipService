using DataModel.Models;

namespace InternshipService.DTO
{
	public record TagDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public TagDto(Tag tag)
		{
			Id = tag.Guid;
			Name = tag.Name;
		}
	}
}
