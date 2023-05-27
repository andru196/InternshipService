using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternRequestDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Tribe { get; set; }
		public string TasksDescription { get; set; }
		public string Email { get; set; }
		public string? Address { get; set; }
		public Guid OrganizationId { get; set; }
		public OrganizationDto? Organization { get; set; }
		public IEnumerable<LinkDto>? Links { get; set; }
		public InternshipDirectionDto Direction {  get; set; }

		public InternRequestDto(InternRequest request, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = request.Guid;
			Name = request.Name;
			Description = request.Description;
			Tribe = request.Tribe;
			TasksDescription = request.TasksDescription;
			Email = request.Email;
			Address = request.Address;
			OrganizationId = request.OrganizationId;
			if (types.Contains(EntityType.Organization) && request.Organization != null)
				Organization = new OrganizationDto(request.Organization);
			if (types.Contains(EntityType.Link))
				Links = request.Links?.Select(x=>new LinkDto(x))?.ToList() ?? Enumerable.Empty<LinkDto>();
			if (types.Contains(EntityType.InternshipDirection))
				Direction = new InternshipDirectionDto(request.Direction);
		}
	}
}
