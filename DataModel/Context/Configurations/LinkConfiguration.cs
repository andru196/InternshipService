using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class LinkConfiguration : NamedEntitiesConfiguration<Link>
	{
		public override void Configure(EntityTypeBuilder<Link> builder)
		{
			builder.HasIndex(x => x.EntityType);
			builder.HasIndex(x => x.ForId);
			builder.HasIndex(x => x.Name);
			base.Configure(builder);
		}
	}
}
