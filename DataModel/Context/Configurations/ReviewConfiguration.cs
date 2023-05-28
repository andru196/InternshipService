using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class ReviewConfiguration : EntitiesConfiguration<Review>
	{
		public override void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.Property(x=>x.TextReview).HasMaxLength(1024).IsRequired();
			builder.HasIndex(x => x.From);
			builder.HasIndex(x => x.To);
			base.Configure(builder);
		}
	}
}
