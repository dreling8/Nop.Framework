using System;
using System.Collections.Generic; 

namespace Nop.Domain.Users
{
    /// <summary>
    /// Represents a user
    /// </summary>
    public partial class User : BaseEntity
    {
        private ICollection<ExternalAuthenticationRecord> _externalAuthenticationRecords;
        private ICollection<UserRole> _userRoles; 

        /// <summary>
        /// Ctor
        /// </summary>
        public User()
        {
            this.UserGuid = Guid.NewGuid();
        }
         
        public Guid UserGuid  { get; set; }
         
        public string Username { get; set; } 

        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the email that should be re-validated. Used in scenarios when a user is already registered and wants to change an email address.
        /// </summary>
        public string EmailToRevalidate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is required to re-login
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }
        /// <summary>
        /// Gets or sets the date and time until which a user cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user account is system
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Gets or sets the user system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }

        #region Navigation properties

        /// <summary>
        /// Gets or sets user generated content
        /// </summary>
        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords
        {
            get { return _externalAuthenticationRecords ?? (_externalAuthenticationRecords = new List<ExternalAuthenticationRecord>()); }
            protected set { _externalAuthenticationRecords = value; }
        }

        /// <summary>
        /// Gets or sets the user roles
        /// </summary>
        public virtual ICollection<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = new List<UserRole>()); }
            protected set { _userRoles = value; }
        }
          
        #endregion
    }
}