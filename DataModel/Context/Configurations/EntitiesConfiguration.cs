using DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal abstract class EntitiesConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.Guid).IsRequired();
			builder.HasIndex(x => x.Guid).IsUnique();
			builder.HasAlternateKey(x => x.Guid);
		}
	}

	internal abstract class NamedEntitiesConfiguration<T> : IEntityTypeConfiguration<T> where T : NamedEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.HasAlternateKey(x => x.Guid);
			builder.Property(x=>x.Name).HasMaxLength(200).IsRequired();
		}
	}
}
