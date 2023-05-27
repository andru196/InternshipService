using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class InternshipDirectionConfiguration : NamedEntitiesConfiguration<InternshipDirection>
	{
		public override void Configure(EntityTypeBuilder<InternshipDirection> builder)
		{
			base.Configure(builder);
			builder.HasAlternateKey(x => x.Name);
		}
	}
}
