namespace DataModel.Models
{
	public record Review: Entity
	{
		public  uint Raiting { get; init; }
		public string TextReview { get; init; }
		public Guid To { get; set; }
		public Guid From { get; set; }
		public ReviewFor ToEntityType { get; set; }
	}
}
