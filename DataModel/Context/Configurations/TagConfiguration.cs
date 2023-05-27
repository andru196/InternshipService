using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class TagConfiguration : NamedEntitiesConfiguration<Tag>
	{
		public override void Configure(EntityTypeBuilder<Tag> builder)
		{
			base.Configure(builder);
			builder.HasIndex(x=>x.Name).IsUnique();
		}
	}
}
