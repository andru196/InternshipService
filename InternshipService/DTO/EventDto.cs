using DataModel.Models;

namespace InternshipService.DTO
{
	public class EventDto : NamedEntityDto
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public OrganizationDto? ByOrganization { get; set; }
		public string? ByOrganizationName { get; set; }
		public DateTime Begin { get; set; }
		public DateTime? End { get; set; }
		
		public EventDto() : base() { }
		public EventDto(Event eventDb, EntityType[] types = null) : base(eventDb)
		{
			types ??= new EntityType[0];
			
			Description = eventDb.Description;
			Address = eventDb.Address;
			ByOrganizationName = eventDb.ByOrganizationName;
			Begin = eventDb.Begin;
			End = eventDb.End;
			if (types.Contains(EntityType.Organization) && eventDb.ByOrganization != null)
				ByOrganization = new OrganizationDto(eventDb.ByOrganization, types);
		}
	}
}
