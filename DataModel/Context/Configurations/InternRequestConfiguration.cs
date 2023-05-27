using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class InternRequestConfiguration : NamedEntitiesConfiguration<InternRequest>
	{
		public override void Configure(EntityTypeBuilder<InternRequest> builder)
		{
			builder.Property(x => x.Description).HasMaxLength(4096);
			builder.Property(x => x.Tribe).HasMaxLength(256);
			builder.Property(x => x.TasksDescription).HasMaxLength(2048);
			builder.Property(x => x.Email).HasMaxLength(128);
			builder.Property(x => x.Address).HasMaxLength(512);
			builder.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).HasPrincipalKey(x => x.Guid);
			builder.HasOne(x=>x.CreatedBy).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.CreatedByGuid);
			builder.HasOne(x => x.Direction).WithMany().HasForeignKey(x => x.DirectionId).HasPrincipalKey(x=>x.Guid);
			base.Configure(builder);
		}
	}
}
