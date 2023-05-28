using DataModel.Models;

namespace InternshipService.DTO
{
	public abstract class EntityDto
	{
		public Guid? Guid { get; set; }
		public bool? IsDeleted { get; set; } = false;
		public EntityDto() => IsDeleted = false;
		public EntityDto(Entity entity) => (this.Guid, IsDeleted) = (entity.Guid, entity.IsDeleted);
	}

	public abstract class NamedEntityDto : EntityDto
	{
		public string? Name { get; set; }
		public NamedEntityDto() => IsDeleted = false;
		public NamedEntityDto(NamedEntity entity) : base(entity) => this.Name = entity.Name;
	}
}
