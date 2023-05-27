namespace DataModel.Models
{
	public abstract record Entity
	{
		public long Id { get; set; }
		public Guid Guid { get; set; }
	}

	public abstract record NamedEntity : Entity
	{
		public string Name { get; set; }
	}
}
