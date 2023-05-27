using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class UniversityConfiguration : NamedEntitiesConfiguration<University>
	{
		public override void Configure(EntityTypeBuilder<University> builder)
		{
			builder.HasOne(x => x.Avatar).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x=>x.AvatarId);
			base.Configure(builder);
		}
	}
}
