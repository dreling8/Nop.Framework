 
using Nop.Domain.Test;

namespace Nop.Data.Mapping.Test
{
    public class TestEntityMap : NopEntityTypeConfiguration<TestEntity>
    {
        public TestEntityMap()
        {
            this.ToTable("TestTable");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);  
        }
    }
}
