using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternshipDirectionDto : NamedEntityDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }


		public InternshipDirectionDto(InternshipDirection direction) : base(direction)
		{
		}
	}
}
