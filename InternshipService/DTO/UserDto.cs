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
			//Password = user.Password;
			Type = user.Type;
		}
	}

	public class UserInternDto: UserDto
	{
		public UserInternDto(User user, InternDto intern) : base(user)
		{
			Intern = intern;
		}
		InternDto Intern { get; set; } 
	}

	public class UserBuddyDto : UserDto
	{
		BuddyDto Buddy { get; set; }
		public UserBuddyDto(User user, BuddyDto buddy) : base(user)
		{
			Buddy = buddy;
		}
	}

	public class UserMentorDto : UserDto
	{
		MentorDto Mentor { get; set; }
		public UserMentorDto(User user, MentorDto mentor) : base(user)
		{
			Mentor = mentor;
		}
	}

	public class UserOrganizationAdminDto : UserDto
	{
		OrganizationAdminDto Admin { get; set; }
		public UserOrganizationAdminDto(User user, OrganizationAdminDto admin) : base(user)
		{
			Admin = admin;
		}
	}


}
