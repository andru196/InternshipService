namespace DataModel.Models
{
	public record User : Entity
	{
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string? MiddleName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public UserType Type { get; set; }
	}
}
