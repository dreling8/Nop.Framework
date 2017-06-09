using System.Collections.Generic;
using Nop.Domain.Users;

namespace Nop.Services.Users
{
    /// <summary>
    /// User attribute parser interface
    /// </summary>
    public partial interface IUserAttributeParser
    {
        /// <summary>
        /// Gets selected user attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Selected user attributes</returns>
        IList<UserAttribute> ParseUserAttributes(string attributesXml);

        /// <summary>
        /// Get user attribute values
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>User attribute values</returns>
        IList<UserAttributeValue> ParseUserAttributeValues(string attributesXml);

        /// <summary>
        /// Gets selected user attribute value
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="userAttributeId">User attribute identifier</param>
        /// <returns>User attribute value</returns>
        IList<string> ParseValues(string attributesXml, int userAttributeId);

        /// <summary>
        /// Adds an attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="ca">User attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes</returns>
        string AddUserAttribute(string attributesXml, UserAttribute ca, string value);

        /// <summary>
        /// Validates user attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Warnings</returns>
        IList<string> GetAttributeWarnings(string attributesXml);
    }
}
