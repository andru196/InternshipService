using DataModel.Models;

namespace InternshipService.DTO
{
	public class UserTrainingDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Organization { get; set; }
		public uint Hours { get; set; }
		public UserDto User { get; set; }
		public FileRecordDto Certificate { get; set; }

		public UserTrainingDto(UserTraining training, EntityType[] types = null)
		{
			types ??= new EntityType[0];
			Id = training.Guid;
			UserId = training.UserId;
			Name = training.Name;
			Description = training.Description;
			Organization = training.Organization;
			Hours = training.Hours;
			if (types.Contains(EntityType.File) && training.Certificate != null) 
				Certificate = new FileRecordDto(training.Certificate);
			if (types.Contains(EntityType.User) && training.User != null)
				User = new UserDto(training.User);
		}
	}
}
