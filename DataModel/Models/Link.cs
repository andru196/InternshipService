namespace DataModel.Models
{
	public record Link : NamedEntity
	{
		public Guid OrgraniztionId { get; set; }
		public virtual Organization? Orgraniztion { get; set; }
	}
}
