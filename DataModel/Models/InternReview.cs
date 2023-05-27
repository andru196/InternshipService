namespace DataModel.Models
{
	public record InternReview: Entity
	{
		public  bool IsLike { get; init; }
		public string TextReview { get; init; }
		public Guid InternId { get; set; }
		public Guid From { get; set; }
	}
}
