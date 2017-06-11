using System;
using System.Text;
using System.Web;
using Nop.Core;
using Nop.Core.Html;
using Nop.Domain.Common; 
using Nop.Services.Localization;

namespace Nop.Services.Users
{
    /// <summary>
    /// User attributes formatter
    /// </summary>
    public partial class UserAttributeFormatter : IUserAttributeFormatter
    {
        #region Fields

        private readonly IUserAttributeParser _userAttributeParser;
        private readonly IUserAttributeService _userAttributeService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public UserAttributeFormatter(IUserAttributeParser userAttributeParser,
            IUserAttributeService userAttributeService,
            IWorkContext workContext)
        {
            this._userAttributeParser = userAttributeParser;
            this._userAttributeService = userAttributeService;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="serapator">Serapator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <returns>Attributes</returns>
        public virtual string FormatAttributes(string attributesXml, string serapator = "<br />", bool htmlEncode = true)
        {
            var result = new StringBuilder();

            var attributes = _userAttributeParser.ParseUserAttributes(attributesXml);
            for (int i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var valuesStr = _userAttributeParser.ParseValues(attributesXml, attribute.Id);
                for (int j = 0; j < valuesStr.Count; j++)
                {
                    string valueStr = valuesStr[j];
                    string formattedAttribute = "";
                    if (!attribute.ShouldHaveValues())
                    {
                        //no values
                        if (attribute.AttributeControlType == AttributeControlType.MultilineTextbox)
                        {
                            //multiline textbox
                            var attributeName = attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id);
                            //encode (if required)
                            if (htmlEncode)
                                attributeName = HttpUtility.HtmlEncode(attributeName);
                            formattedAttribute = string.Format("{0}: {1}", attributeName, HtmlHelper.FormatText(valueStr, false, true, false, false, false, false));
                            //we never encode multiline textbox input
                        }
                        else if (attribute.AttributeControlType == AttributeControlType.FileUpload)
                        {
                            //file upload
                            //not supported for user attributes
                        }
                        else
                        {
                            //other attributes (textbox, datepicker)
                            formattedAttribute = string.Format("{0}: {1}", attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id), valueStr);
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = HttpUtility.HtmlEncode(formattedAttribute);
                        }
                    }
                    else
                    {
                        int attributeValueId;
                        if (int.TryParse(valueStr, out attributeValueId))
                        {
                            var attributeValue = _userAttributeService.GetUserAttributeById(attributeValueId);
                            if (attributeValue != null)
                            {
                                formattedAttribute = string.Format("{0}: {1}", attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id), attributeValue.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id));
                            }
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = HttpUtility.HtmlEncode(formattedAttribute);
                        }
                    }

                    if (!String.IsNullOrEmpty(formattedAttribute))
                    {
                        if (i != 0 || j != 0)
                            result.Append(serapator);
                        result.Append(formattedAttribute);
                    }
                }
            }

            return result.ToString();
        }

        #endregion
    }
}
