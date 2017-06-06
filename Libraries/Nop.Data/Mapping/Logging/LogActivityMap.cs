 
using Nop.Domain.Logging;

namespace Nop.Data.Mapping.Logging
{
    public partial class LogActivityMap : NopEntityTypeConfiguration<ActivityLog>
    {
        public LogActivityMap()
        {
            this.ToTable("ActivityLog");
            this.HasKey(al => al.Id);
            this.Property(al => al.Comment).IsRequired();
            this.Property(al => al.IpAddress).HasMaxLength(200);

            this.HasRequired(al => al.ActivityLogType)
                .WithMany()
                .HasForeignKey(al => al.ActivityLogTypeId);

            this.HasRequired(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId);
        }
    }
}
