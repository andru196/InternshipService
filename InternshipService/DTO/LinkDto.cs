using DataModel.Models;

namespace InternshipService.DTO
{
	public class LinkDto
	{
		public Guid Id { get; set; }
		public string Url { get; set; }
		public Guid OrgranizationId { get; set; }
		public OrganizationDto? Orgraniztion { get; set; }
		public LinkDto(Link link, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = link.Guid;
			Url = link.Name;
			OrgranizationId = link.OrgraniztionId;
			if (types.Contains(EntityType.Organization) && link.Orgraniztion != null) 
				Orgraniztion = new OrganizationDto(link.Orgraniztion);
		}
	}
}
