﻿namespace DataModel.Models
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
		None,
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

	public enum InternResponseStatus
	{
		New,
		Sent,
		Moderated,
		InviteForCareerSchool,
		InviteForTest,
		InviteForCaseChempionship,
		InviteForInterview,
		Approved,
		Canceled,
		Rejected
	}

	public enum InternRequestStatus
	{
		New,
		Sent,
		Approved,
		Finished,
		Rejected
	}

	public enum Sex
	{
		Male,
		Female
	}

	public enum EducationDegree
	{
		Bachelor,
		Master,
		Specialist,
		PostGraduate
	}

	public enum ReviewFor
	{
		Buddy,
		Intern,
		Company
	}

}
