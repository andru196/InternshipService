using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class InternResponseConfiguration : EntitiesConfiguration<InternResponse>
	{
		public override void Configure(EntityTypeBuilder<InternResponse> builder)
		{
			builder.Property(x => x.Message).IsRequired(false);
			
			builder.HasOne(x => x.Intern).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.InternId);
			builder.HasOne(x => x.CV).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.CVGuid);
			builder.HasOne(x=>x.InternRequest).WithMany().HasPrincipalKey(x=>x.Guid).HasForeignKey(x=>x.InternRequestId).IsRequired();
			builder.HasIndex(x => new { x.InternId, x.InternRequestId }).IsUnique();
			base.Configure(builder);
		}
	}
}
