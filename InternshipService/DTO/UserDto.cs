using DataModel.Models;

namespace InternshipService.DTO
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string? MiddleName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public UserType Type { get; set; }

		public UserDto(User user)
		{
			Id = user.Guid;
			FirstName = user.FirstName;
			SecondName = user.SecondName;
			MiddleName = user.MiddleName;
			Email = user.Email;
			Password = user.Password;
			Type = user.Type;
		}
	}
}
