using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class LinkConfiguration : NamedEntitiesConfiguration<Link>
	{
		public override void Configure(EntityTypeBuilder<Link> builder)
		{
			builder.HasOne(x => x.Orgraniztion).WithMany().HasForeignKey(x => x.OrgraniztionId).HasPrincipalKey(x => x.Guid);
			base.Configure(builder);
		}
	}
}
