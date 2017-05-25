using System;

namespace Nop.Domain.Test
{
    public class TestEntity: BaseEntity
    { 

        public virtual string  Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? CreateDate { get; set; }
         
    }
}
