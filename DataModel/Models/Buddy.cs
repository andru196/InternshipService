namespace DataModel.Models
{
	public record Buddy: Entity
	{
		public Guid UserId { get; set; }
		public virtual User? User { get; set; }
		public virtual FileRecord? Avatar { get; set; }
		public Guid OrganizationId { get; set; }
		public Guid? AvatarGuid { get; set; }
		public double Raiting { get; set; } = 0;
		public virtual Organization Organization { get; set; }
		// TODO: получает login/password
		// TODO: видит статусы заявок на стажёров (от своей орг-ии)
		// TODO: может назначить собес стажёру
		// TODO: отобряет откланяет отклик от стажёра
		// TODO: видит программу развития
	}
}
