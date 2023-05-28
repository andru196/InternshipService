namespace DataModel.Models
{
	public record Mentor : Entity
	{
		public virtual User? User { get; set; }
		public Guid UserId { get; set; }
		public virtual InternshipDirection Direction { get; set; }
		public Guid DirectionId { get; set; }

		// TODO: должен устанавливать курсы, тесты (из сущ-их)
		// TODO: видит рейтинг стажёров
		// TODO: видит список наставников, прошедших обучение в школе наставников
		// TODO: видит отзывы

	}
}
