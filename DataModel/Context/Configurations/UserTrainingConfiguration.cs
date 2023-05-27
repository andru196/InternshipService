using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class UserTrainingConfiguration : NamedEntitiesConfiguration<UserTraining>
	{
		public override void Configure(EntityTypeBuilder<UserTraining> builder)
		{
			builder.HasOne(x=>x.User).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.UserId);
			builder.HasOne(x => x.Certificate).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.CertificateId);
			base.Configure(builder);
		}
	}
}
