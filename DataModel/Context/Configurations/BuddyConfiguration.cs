using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class BuddyConfiguration : EntitiesConfiguration<Buddy>
	{
		public override void Configure(EntityTypeBuilder<Buddy> builder)
		{
			builder.HasOne(x => x.Avatar)
				.WithMany()
				.HasPrincipalKey(x => x.Guid)
				.HasForeignKey(x => x.AvatarGuid);
			builder.HasOne(x => x.Organization)
				.WithMany()
				.HasPrincipalKey(x => x.Guid)
				.HasForeignKey(x => x.OrganizationId);
			builder.HasOne(x=>x.User)
				.WithMany()
				.HasPrincipalKey(x=>x.Guid)
				.HasForeignKey(x => x.UserId);
			builder.HasIndex(x => x.UserId)
				.IsUnique();
			base.Configure(builder);
		}
	}
}
