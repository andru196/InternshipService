using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class MentorConfiguration : EntitiesConfiguration<Mentor>
	{
		public override void Configure(EntityTypeBuilder<Mentor> builder)
		{
			builder.HasOne(x => x.User).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.Direction).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x=>x.DirectionId);

			builder.HasIndex(x => x.UserId).IsUnique();
			base.Configure(builder);
		}
	}
}
