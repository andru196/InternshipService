using DataModel.Models;

namespace InternshipService.DTO
{
	public record ReviewDto
	{
		public Guid Id { get; set; }
		public Guid InternId { get; set; }
		public Guid From { get; set; }
		public  bool IsLike { get; init; }
		public string TextReview { get; init; }

		public ReviewDto(Review review)
		{
			Id = review.Guid;
			InternId = review.InternId;
			From = review.From;
			TextReview = review.TextReview;
			IsLike = review.IsLike;
		}
	}
}
