using System.Collections.Generic;
using Nop.Domain.Users;

namespace Nop.Services.Users
{
    /// <summary>
    /// User attribute service
    /// </summary>
    public partial interface IUserAttributeService
    {
        /// <summary>
        /// Deletes a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        void DeleteUserAttribute(UserAttribute userAttribute);

        /// <summary>
        /// Gets all user attributes
        /// </summary>
        /// <returns>User attributes</returns>
        IList<UserAttribute> GetAllUserAttributes();

        /// <summary>
        /// Gets a user attribute 
        /// </summary>
        /// <param name="userAttributeId">User attribute identifier</param>
        /// <returns>User attribute</returns>
        UserAttribute GetUserAttributeById(int userAttributeId);

        /// <summary>
        /// Inserts a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        void InsertUserAttribute(UserAttribute userAttribute);

        /// <summary>
        /// Updates the user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        void UpdateUserAttribute(UserAttribute userAttribute);

        /// <summary>
        /// Deletes a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        void DeleteUserAttributeValue(UserAttributeValue userAttributeValue);

        /// <summary>
        /// Gets user attribute values by user attribute identifier
        /// </summary>
        /// <param name="userAttributeId">The user attribute identifier</param>
        /// <returns>User attribute values</returns>
        IList<UserAttributeValue> GetUserAttributeValues(int userAttributeId);

        /// <summary>
        /// Gets a user attribute value
        /// </summary>
        /// <param name="userAttributeValueId">User attribute value identifier</param>
        /// <returns>User attribute value</returns>
        UserAttributeValue GetUserAttributeValueById(int userAttributeValueId);

        /// <summary>
        /// Inserts a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        void InsertUserAttributeValue(UserAttributeValue userAttributeValue);

        /// <summary>
        /// Updates the user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        void UpdateUserAttributeValue(UserAttributeValue userAttributeValue);
    }
}
