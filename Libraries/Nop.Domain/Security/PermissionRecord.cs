using System.Collections.Generic; 
using Nop.Domain.Users;

namespace Nop.Domain.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public partial class PermissionRecord : BaseEntity
    {
        private ICollection<UserRole> _userRoles;

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }
        
        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = new List<UserRole>()); }
            protected set { _userRoles = value; }
        }   
    }
}
