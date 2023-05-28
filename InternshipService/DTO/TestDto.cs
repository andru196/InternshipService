using DataModel.Models;

namespace InternshipService.DTO
{
	public class TestDto : NamedEntityDto
	{
		public bool IsLink { get; set; }
		public string? Body { get; set; }
		public Guid OrganizationId { get; set; }

		public TestDto(Test test) : base(test)
		{
			IsLink = test.IsLink;
			Body = test.Body;
			OrganizationId = test.OrganizationId;
		}
	}
}
