using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class InternConfiguration : EntitiesConfiguration<Intern>
	{
		public override void Configure(EntityTypeBuilder<Intern> builder)
		{
			builder.Property(x => x.UserId).IsRequired();
			builder.Property(x => x.About).HasMaxLength(2048);

			builder.HasMany(x => x.Tags).WithMany();
			
			builder.HasOne(x => x.Avatar).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.AvatarId);
			builder.HasOne(x => x.University).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.UniversityId);
			builder.HasOne(x => x.User).WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.UserId);
			builder.HasIndex(x => x.UserId).IsUnique();
			base.Configure(builder);
		}
	}
}
