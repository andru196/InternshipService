using DataModel.Models;

namespace InternshipService.DTO
{
	public class FileRecordDto : EntityDto
	{
		public string FullPath { get; set; }

		public FileRecordDto(FileRecord file) : base(file)
		{
			//FullPath = file.FullPath;
		}
	}
}
