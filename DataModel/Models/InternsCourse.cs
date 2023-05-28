using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
	public record InternsCourse : Entity
	{
		public Guid InterId { get; set; }
		public Guid CourseId { get; set; }

		public Intern? Inter { get; set; }
		public Course? Course { get; set; }
		public bool IsViewed { get; set; }
	}
}
