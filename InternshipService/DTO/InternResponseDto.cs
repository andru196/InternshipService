using DataModel.Models;

namespace InternshipService.DTO
{
	public record InternResponseDto
	{
		public Guid Id { get; set; }
		public Guid InternId { get; set; }
		public string Message { get; set; }
		public InternDto Intern { get; set; }
		public FileRecordDto? CV { get; set; }

		public InternResponseDto(InternResponse response, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = response.Guid;
			Message = response.Message;
			InternId = response.InternId;
			if (types.Contains(EntityType.File) && response.CV != null)
				CV = new FileRecordDto(response.CV);
			if (types.Contains(EntityType.Intern) && response.Intern != null)
				Intern = new InternDto(response.Intern);
		}
	}
}
