using DataModel.Models;

namespace InternshipService.DTO
{
	public class FileRecordDto
	{
		public Guid Id { get; set; }
		public string FullPath { get; set; }

		public FileRecordDto(FileRecord file)
		{
			Id = file.Guid;
			FullPath = file.FullPath;
		}
	}
}
