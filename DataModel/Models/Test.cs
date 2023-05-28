namespace DataModel.Models
{
	public record Test : NamedEntity
	{
		public bool IsLink { get; set; }
		public string? Body { get; set; }
	}
}
