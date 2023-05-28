using DataModel.Models;

namespace InternshipService.DTO
{
	public class ReviewDto : EntityDto
	{
		public Guid Id { get; set; }
		public Guid To { get; set; }
		public Guid From { get; set; }
		public string TextReview { get; init; }
		public uint Raiting { get; init; }
		public ReviewFor ToEntityType { get; set; }

		public ReviewDto(Review review) : base(review)
		{
			Id = review.Guid;
			To = review.To;
			From = review.From;
			TextReview = review.TextReview;
			Raiting = review.Raiting;
			ToEntityType = review.ToEntityType;
		}
	}
}
