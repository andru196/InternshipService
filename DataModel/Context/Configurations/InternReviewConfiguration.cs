using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class InternReviewConfiguration : EntitiesConfiguration<InternReview>
	{
		public override void Configure(EntityTypeBuilder<InternReview> builder)
		{
			builder.Property(x=>x.TextReview).HasMaxLength(1024).IsRequired();
			builder.HasOne<Intern>().WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.InternId);
			builder.HasOne<Buddy>().WithMany().HasPrincipalKey(x => x.Guid).HasForeignKey(x => x.From);
			base.Configure(builder);
		}
	}
}
