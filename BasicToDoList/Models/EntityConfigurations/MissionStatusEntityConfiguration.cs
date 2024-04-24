using BasicToDoList.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicToDoList.Models.EntityConfigurations
{
    public class MissionStatusEntityConfiguration : IEntityTypeConfiguration<MissionStatus>
    {
        public void Configure(EntityTypeBuilder<MissionStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
