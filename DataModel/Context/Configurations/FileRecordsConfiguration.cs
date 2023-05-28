using DataModel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Context.Configurations
{
	internal class FileRecordsConfiguration : EntitiesConfiguration<FileRecord>
	{
		public override void Configure(EntityTypeBuilder<FileRecord> builder)
		{
			builder.Property(x => x.Path).HasMaxLength(300).IsRequired();
			base.Configure(builder);
		}
	}
}
