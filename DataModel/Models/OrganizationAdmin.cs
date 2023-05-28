namespace DataModel.Models
{
	public record OrganizationAdmin : Entity
	{
		public Guid UserId { get; set; }
		public Guid OrganizationId { get; set; }
		public virtual Organization? Organization { get; set; }
		public virtual User? User{ get; set; }

		// TODO: видит список наставников, прошедших обучение в школе наставников
		// TODO: публикует тестовое задание для конкретной заявки
		// TODO: видит статусы заявок на его заявки\отклики
		// TODO: видит результаты тестовых заданий
		// TODO: апрувит стажёра и наставника
	}
}
