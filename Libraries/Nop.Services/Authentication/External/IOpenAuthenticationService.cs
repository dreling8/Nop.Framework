//Contributor:  Nicholas Mayne

using System.Collections.Generic;
using Nop.Domain.Users;

namespace Nop.Services.Authentication.External
{
    /// <summary>
    /// Open authentication service
    /// </summary>
    public partial interface IOpenAuthenticationService
    {
        #region External authentication methods

        /// <summary>
        /// Load active external authentication methods
        /// </summary>
        /// <param name="user">Load records allowed only to a specified user; pass null to ignore ACL permissions</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Payment methods</returns>
        IList<IExternalAuthenticationMethod> LoadActiveExternalAuthenticationMethods(User user = null, int storeId = 0);

        /// <summary>
        /// Load external authentication method by system name
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Found external authentication method</returns>
        IExternalAuthenticationMethod LoadExternalAuthenticationMethodBySystemName(string systemName);

        /// <summary>
        /// Load all external authentication methods
        /// </summary>
        /// <param name="user">Load records allowed only to a specified user; pass null to ignore ACL permissions</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>External authentication methods</returns>
        IList<IExternalAuthenticationMethod> LoadAllExternalAuthenticationMethods(User user = null, int storeId = 0);

        #endregion

        /// <summary>
        /// Accociate external account with user
        /// </summary>
        /// <param name="user">Customer</param>
        /// <param name="parameters">Open authentication parameters</param>
        void AssociateExternalAccountWithUser(User user, OpenAuthenticationParameters parameters);

        /// <summary>
        /// Check that account exists
        /// </summary>
        /// <param name="parameters">Open authentication parameters</param>
        /// <returns>True if it exists; otherwise false</returns>
        bool AccountExists(OpenAuthenticationParameters parameters);

        /// <summary>
        /// Get the particular user with specified parameters
        /// </summary>
        /// <param name="parameters">Open authentication parameters</param>
        /// <returns>Customer</returns>
        User GetUser(OpenAuthenticationParameters parameters);

        /// <summary>
        /// Get external authentication records for the specified user
        /// </summary>
        /// <param name="user">Customer</param>
        /// <returns>List of external authentication records</returns>
        IList<ExternalAuthenticationRecord> GetExternalIdentifiersFor(User user);

        /// <summary>
        /// Delete the external authentication record
        /// </summary>
        /// <param name="externalAuthenticationRecord">External authentication record</param>
        void DeleteExternalAuthenticationRecord(ExternalAuthenticationRecord externalAuthenticationRecord);

        /// <summary>
        /// Remove the association
        /// </summary>
        /// <param name="parameters">Open authentication parameters</param>
        void RemoveAssociation(OpenAuthenticationParameters parameters);
    }
}