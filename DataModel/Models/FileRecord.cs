namespace DataModel.Models
{
	public record FileRecord : Entity
	{
		public string Path { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
	}
}
