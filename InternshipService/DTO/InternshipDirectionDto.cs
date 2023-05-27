using DataModel.Models;

namespace InternshipService.DTO
{
	public record InternshipDirectionDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public InternshipDirectionDto(InternshipDirection direction)
		{
			Id = direction.Guid;
			Name = direction.Name;
		}
	}
}
