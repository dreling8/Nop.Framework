using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Domain.Configuration;

namespace Nop.Domain.Common
{
    public class CommonSettings : ISettings
    {
        public CommonSettings()
        { 
            IgnoreLogWordlist = new List<string>();
        }
          
        /// <summary>
        /// Gets or sets a value indicating whether stored procedures are enabled (should be used if possible)
        /// </summary>
        public bool UseStoredProceduresIfSupported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use stored procedure (if supported) for loading categories (it's much faster in admin area with a large number of categories than the LINQ implementation)
        /// </summary>
        public bool UseStoredProcedureForLoadingCategories { get; set; }
           
        /// <summary>
        /// Gets or sets a value indicating whether 404 errors (page or file not found) should be logged
        /// </summary>
        public bool Log404Errors { get; set; }
          
        /// <summary>
        /// Gets or sets ignore words (phrases) to be ignored when logging errors/messages
        /// </summary>
        public List<string> IgnoreLogWordlist { get; set; }


        public bool BbcodeEditorOpenLinksInNewWindow { get; set; }


    }
}