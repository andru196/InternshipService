using DataModel.Models;

namespace InternshipService.DTO
{
	public class LinkDto : EntityDto
	{
		public Guid ForId { get; set; }
		public EntityType EntityType { get; set; }
		public string Url { get; set; }
		public LinkDto() : base() { }
		public LinkDto(Link link) : base(link)
		{
			ForId = link.ForId;
			Url = link.Name;
			EntityType = link.EntityType;
		}
	}
}
