using DataModel.Models;

namespace InternshipService.DTO
{
	public abstract class EntityDto
	{
		public Guid Id { get; set; }
		public bool IsDeleted { get; set; }
		public EntityDto(Entity entity) => (this.Id, IsDeleted) = (entity.Guid, entity.IsDeleted);
	}

	public abstract class NamedEntityDto : EntityDto
	{
		public string Name { get; set; }
		public NamedEntityDto(NamedEntity entity) : base(entity) => this.Name = entity.Name;
	}
}
