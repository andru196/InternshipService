using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class OrganizationAdminConfiguration : EntitiesConfiguration<OrganizationAdmin>
	{
		public override void Configure(EntityTypeBuilder<OrganizationAdmin> builder)
		{
			builder.HasOne(x => x.User).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.Organization).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.OrganizationId);
			builder.HasIndex(x => x.OrganizationId);
			builder.HasIndex(x => x.UserId).IsUnique();
			base.Configure(builder);
		}
	}
}
