﻿namespace DataModel.Models
{
	public record Intern : Entity
	{
		public Guid UserId { get; set; }
		public virtual User User { get; set; }
		public DateTime BirthDate { get; set; }
		public string About { get; set; }
		public Guid? AvatarId { get; set; }
		public Guid? UniversityId { get; set; }
		public short StartOfEducation { get; set; }
		public short EndOfEducation { get; set; }
		public EducationDegree EducationDegree { get; set; }
		public string EducationSpecialiazation { get; set; }
		public Sex Sex { get; set; }
		public string FederalDistrict { get; set; }
		public string Citizenship { get; set; }
		public InternStatus Status { get; set; }
		public virtual University? University { get; set; }
		public virtual FileRecord? Avatar { get; set; }
		public virtual IEnumerable<UserEvent> Events { get; set; }
		public virtual IEnumerable<Tag> Tags { get; set; }
	}
}
