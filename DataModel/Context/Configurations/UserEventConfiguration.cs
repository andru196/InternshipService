using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class UserEventConfiguration : EntitiesConfiguration<UserEvent>
	{
		public override void Configure(EntityTypeBuilder<UserEvent> builder)
		{
			builder.HasOne(x=>x.User).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.UserId);
			builder.HasOne(x=>x.Event).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.EventId);
			base.Configure(builder);
		}
	}
}
