namespace DataModel.Models
{
	public record ArrivalsFromChannel : Entity
	{
		public string Channel { get; set; }
		public Guid UserId { get; set; }
	}
}
