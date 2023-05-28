using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class UsersConfiguration : EntitiesConfiguration<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			base.Configure(builder);
			builder.Property(x=>x.Email).HasMaxLength(250).IsRequired();
			builder.Property(x=>x.Password).IsRequired();
			builder.Property(x=>x.FirstName).IsRequired();
			builder.Property(x=>x.SecondName).IsRequired();
			builder.Property(x=>x.Type).IsRequired();

			builder.HasIndex(x=> x.Email).IsUnique();
			builder.HasIndex(x=> x.Phone).IsUnique();
		}
	}
}
