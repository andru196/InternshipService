namespace DataModel.Models
{
	// TODO: сделать проверку, если это опрос
	public record InternTest : Entity
	{
		public Guid InterId { get; set; }
		public Guid TestId { get; set; }

		public Intern? Inter { get; set; }
		public Test? Test { get; set; }
		public bool IsViewed { get; set; }
	}
}
