using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternRequestDto : NamedEntityDto
	{
		public string Description { get; set; }
		public string Tribe { get; set; }
		public string TasksDescription { get; set; }
		public string Email { get; set; }
		public string? Address { get; set; }
		public Guid OrganizationId { get; set; }
		public Guid CreatedByGuid { get; set; }
		public Guid DirectionId {  get; set; }
		public double Latitude {  get; set; }
		public double Longitude {  get; set; }
		public InternRequestStatus Status { get; set; }
		public DateTime? CreatedDate { get; set; }
		public OrganizationDto? Organization { get; set; }
		public IEnumerable<LinkDto>? Links { get; set; }
		public InternshipDirectionDto? Direction {  get; set; }
		public OrganizationAdminDto? CreatedBy { get; set; }

		public InternRequestDto() : base() { }
		public InternRequestDto(InternRequest request, EntityType[] types = null)
			: base(request)
		{
			types ??= new EntityType[0];
			Description = request.Description;
			Tribe = request.Tribe;
			TasksDescription = request.TasksDescription;
			Email = request.Email;
			Address = request.Address;
			OrganizationId = request.OrganizationId;
			CreatedByGuid = request.CreatedByGuid;
			DirectionId = request.DirectionId;
			Latitude = request.Latitude;
			Longitude = request.Longitude;
			Status = request.Status;

			if (types.Contains(EntityType.Organization) && request.Organization != null)
				Organization = new OrganizationDto(request.Organization, types);
			if (types.Contains(EntityType.Link))
				Links = request.Links?.Select(x=>new LinkDto(x))?.ToList() ?? Enumerable.Empty<LinkDto>();
			if (types.Contains(EntityType.InternshipDirection))
				Direction = new InternshipDirectionDto(request.Direction);
			if (types.Contains(EntityType.OrganizationAdmin) && request.CreatedBy != null)
				CreatedBy = new OrganizationAdminDto(request.CreatedBy, types);
		}
	}
}
