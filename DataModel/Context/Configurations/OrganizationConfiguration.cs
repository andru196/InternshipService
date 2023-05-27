using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class OrganizationConfiguration : NamedEntitiesConfiguration<Organization>
	{
		public override void Configure(EntityTypeBuilder<Organization> builder)
		{
			builder.HasMany(x=>x.Admins).WithOne(x => x.Organization).HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.OrganizationId);
			base.Configure(builder);
		}
	}
}
