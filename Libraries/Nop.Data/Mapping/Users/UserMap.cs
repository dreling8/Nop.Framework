 
using Nop.Domain.Users;

namespace Nop.Data.Mapping.Users
{
    public partial class UserMap : NopEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(c => c.Id);
            this.Property(u => u.Username).HasMaxLength(1000);
            this.Property(u => u.Email).HasMaxLength(1000);
            this.Property(u => u.EmailToRevalidate).HasMaxLength(1000);
            this.Property(u => u.SystemName).HasMaxLength(400);
            
            this.HasMany(c => c.UserRoles)
                .WithMany()
                .Map(m => m.ToTable("User_UserRole_Mapping"));
             
        }
    }
}