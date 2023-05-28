using DataModel.Models;

namespace InternshipService.DTO
{
	public class InternTestDto : EntityDto
	{
		public Guid InterId { get; set; }
		public Guid TestId { get; set; }
		public bool IsViewed { get; set; }

		public InternDto? Intern { get; set; }
		public TestDto? Test { get; set; }
		public InternTestDto(InternTest test, EntityType[] types = null) : base(test)
		{
			types ??= new EntityType[0];

			InterId = test.InterId;
			TestId = test.TestId;
			IsViewed = test.IsViewed;

			if (types.Contains(EntityType.Test) && test.Test != null)
				Test = new TestDto(test.Test);
			if (types.Contains(EntityType.Intern) && test.Intern != null)
				Intern = new InternDto(test.Intern, types);
		}
	}
}
