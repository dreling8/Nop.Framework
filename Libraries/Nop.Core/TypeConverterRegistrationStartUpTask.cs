using System.Collections.Generic;
using System.ComponentModel;
using Nop.Core.ComponentModel; 
using Nop.Core.Infrastructure;

namespace Nop.Core
{
    /// <summary>
    /// Startup task for the registration custom type converters
    /// </summary>
    public class TypeConverterRegistrationStartUpTask : IStartupTask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            //lists
            TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            TypeDescriptor.AddAttributes(typeof(List<decimal>), new TypeConverterAttribute(typeof(GenericListTypeConverter<decimal>)));
            TypeDescriptor.AddAttributes(typeof(List<string>), new TypeConverterAttribute(typeof(GenericListTypeConverter<string>)));

            //dictionaries
            TypeDescriptor.AddAttributes(typeof(Dictionary<int, int>), new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<int, int>)));
  
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
