using System.Collections.Generic;
using Nop.Domain.Common;
using Nop.Domain.Localization;

namespace Nop.Domain.Users
{
    /// <summary>
    /// Represents a customer attribute
    /// </summary>
    public partial class UserAttribute : BaseEntity, ILocalizedEntity
    {
        private ICollection<UserAttributeValue > _userAttributeValues;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the attribute is required
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the attribute control type identifier
        /// </summary>
        public int AttributeControlTypeId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }




        /// <summary>
        /// Gets the attribute control type
        /// </summary>
        public AttributeControlType AttributeControlType
        {
            get
            {
                return (AttributeControlType)this.AttributeControlTypeId;
            }
            set
            {
                this.AttributeControlTypeId = (int)value;
            }
        }
        /// <summary>
        /// Gets the customer attribute values
        /// </summary>
        public virtual ICollection<UserAttributeValue> UserAttributeValues
        {
            get { return _userAttributeValues ?? (_userAttributeValues = new List<UserAttributeValue>()); }
            protected set { _userAttributeValues = value; }
        }
    }

}
