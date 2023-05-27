namespace DataModel.Models
{
	public enum EntityType
	{
		None,
		User,
		Intern,
		Organization,
		OrganizationAdmin,
		Tag,
		UserTraining,
		UserEvent,
		Event,
		University,
		Mentor,
		Link,
		Buddy,
		InternRequest,
		InternResponse,
		InternshipDirection,
		File
	}

	public enum UserType
	{
		Student,
		Mentor,
		Buddy,
		OrganizationAdmin,
		Admin
	}

	public enum InternStatus 
	{
		Employed,
		Unemployed
	}

	public enum UserEventAttendStatus
	{
		Missed,
		Planned,
		Visited,
		Canceled
	}
}
