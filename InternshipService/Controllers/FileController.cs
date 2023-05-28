using AutoMapper;
using DataModel.Context;
using InternshipService.Configs;
using InternshipService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InternshipService.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FileController : ControllerBasePlusAuth
	{
		private FileStorageConfig _config;
		public FileController(ILogger logger, InternshipsDbContect context, IMapper mapper, FileStorageConfig config) : base(logger, context, mapper) => _config = config;

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(Guid id)
		{
			var fileRecord = _dbContext.Files.FirstOrDefault(x => x.Guid == id);
			if (fileRecord == null) return NotFound();
			FileStream fs = new FileStream(fileRecord.Path, FileMode.Open);
			//string fileType = "application/pdf";
			return File(fs, fileRecord.Type, fileRecord.Name);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<FileRecordDto>> Post([FromQuery]string type, IFormFile uploadedFile)
		{
			var path = Path.Combine(_config.PathToStorage, uploadedFile.FileName);
			var i = 0;
			while (System.IO.File.Exists(path))
				path = Path.Combine(_config.PathToStorage, $"{uploadedFile.FileName}({++i})");
			using (var fileStream = new FileStream(path, FileMode.Create))			
				await uploadedFile.CopyToAsync(fileStream);
			var record = new DataModel.Models.FileRecord
			{
				Guid = Guid.NewGuid(),
				Name = uploadedFile.FileName,
				Type = type,
				Path = path,
			};
			_dbContext.Files.Add(record);
			await _dbContext.SaveChangesAsync();
			return Ok(new FileRecordDto(record));
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(Guid id)
		{
			var fileRecord = _dbContext.Files.FirstOrDefault(x => x.Guid == id);
			_dbContext.Files.Remove(fileRecord);
			await _dbContext.SaveChangesAsync();
			if (System.IO.File.Exists(fileRecord.Path))
				System.IO.File.Delete(fileRecord.Path);
			return Ok();
		}
	}
}
