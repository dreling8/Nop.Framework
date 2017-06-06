using System;
using System.Linq;
using System.Web;
using Nop.Core; 
using Nop.Core.Fakes;
using Nop.Domain.Users; 

namespace Nop.Web.Framework
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string CustomerCookieName = "Nop.customer";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        //private readonly IUserService _customerService;
        //private readonly IAuthenticationService _authenticationService;
        //private readonly ILanguageService _languageService;
        //private readonly LocalizationSettings _localizationSettings;
        //private readonly IUserAgentHelper _userAgentHelper;

        private User _cachedCustomer;
        //private Language _cachedLanguage;

        #endregion

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext
            //ICustomerService customerService,
            //IAuthenticationService authenticationService,
            //ILanguageService languageService,
            //LocalizationSettings localizationSettings,
            //IUserAgentHelper userAgentHelper, 
            )
        {
            this._httpContext = httpContext;
            //this._customerService = customerService;
            //this._authenticationService = authenticationService;
            //this._languageService = languageService;
            //this._localizationSettings = localizationSettings;
            //this._userAgentHelper = userAgentHelper;
        }

        #endregion

        #region Utilities

        protected virtual HttpCookie GetCustomerCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[CustomerCookieName];
        }

        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(CustomerCookieName);
                cookie.HttpOnly = true;
                cookie.Value = customerGuid.ToString();
                if (customerGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(CustomerCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        //protected virtual Language GetLanguageFromUrl()
        //{
        //    if (_httpContext == null || _httpContext.Request == null)
        //        return null;

        //    string virtualPath = _httpContext.Request.AppRelativeCurrentExecutionFilePath;
        //    string applicationPath = _httpContext.Request.ApplicationPath;
        //    if (!virtualPath.IsLocalizedUrl(applicationPath, false))
        //        return null;

        //    var seoCode = virtualPath.GetLanguageSeoCodeFromUrl(applicationPath, false);
        //    if (String.IsNullOrEmpty(seoCode))
        //        return null;

        //    var language = _languageService
        //        .GetAllLanguages()
        //        .FirstOrDefault(l => seoCode.Equals(l.UniqueSeoCode, StringComparison.InvariantCultureIgnoreCase));
        //    if (language != null && language.Published && _storeMappingService.Authorize(language))
        //    {
        //        return language;
        //    }

        //    return null;
        //}

        //protected virtual Language GetLanguageFromBrowserSettings()
        //{
        //    if (_httpContext == null ||
        //        _httpContext.Request == null ||
        //        _httpContext.Request.UserLanguages == null)
        //        return null;

        //    var userLanguage = _httpContext.Request.UserLanguages.FirstOrDefault();
        //    if (String.IsNullOrEmpty(userLanguage))
        //        return null;

        //    var language = _languageService
        //        .GetAllLanguages()
        //        .FirstOrDefault(l => userLanguage.Equals(l.LanguageCulture, StringComparison.InvariantCultureIgnoreCase));
        //    if (language != null && language.Published && _storeMappingService.Authorize(language))
        //    {
        //        return language;
        //    }

        //    return null;
        //}

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>25141240
        public virtual User CurrentUser
        {
            get
            {
                return new User()
                {
                    Id = 1,
                    Username = "Guest",
                    UserGuid = Guid.NewGuid(),
                    CreatedOnUtc =  DateTime.UtcNow,
                    LastActivityDateUtc = DateTime.UtcNow,
                };
                //if (_cachedCustomer != null)
                //    return _cachedCustomer;

                //User user = null;
                //if (_httpContext == null || _httpContext is FakeHttpContext)
                //{
                //    //check whether request is made by a background task
                //    //in this case return built-in customer record for background task
                //    user = _customerService.GetCustomerBySystemName(SystemCustomerNames.BackgroundTask);
                //}

                ////check whether request is made by a search engine
                ////in this case return built-in customer record for search engines 
                ////or comment the following two lines of code in order to disable this functionality
                //if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                //{
                //    if (_userAgentHelper.IsSearchEngine())
                //    {
                //        user = _customerService.GetCustomerBySystemName(SystemCustomerNames.SearchEngine);
                //    }
                //}

                ////registered user
                //if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                //{
                //    user = _authenticationService.GetAuthenticatedCustomer();
                //}

                ////impersonate user if required (currently used for 'phone order' support)
                //if (user != null && !user.Deleted && user.Active && !user.RequireReLogin)
                //{
                //    var impersonatedCustomerId = user.GetAttribute<int?>(SystemCustomerAttributeNames.ImpersonatedCustomerId);
                //    if (impersonatedCustomerId.HasValue && impersonatedCustomerId.Value > 0)
                //    {
                //        var impersonatedCustomer = _customerService.GetCustomerById(impersonatedCustomerId.Value);
                //        if (impersonatedCustomer != null && !impersonatedCustomer.Deleted && impersonatedCustomer.Active && !impersonatedCustomer.RequireReLogin)
                //        {
                //            //set impersonated customer 
                //            user = impersonatedCustomer;
                //        }
                //    }
                //}

                ////load guest customer
                //if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                //{
                //    var customerCookie = GetCustomerCookie();
                //    if (customerCookie != null && !String.IsNullOrEmpty(customerCookie.Value))
                //    {
                //        Guid customerGuid;
                //        if (Guid.TryParse(customerCookie.Value, out customerGuid))
                //        {
                //            var customerByCookie = _customerService.GetCustomerByGuid(customerGuid);
                //            if (customerByCookie != null &&
                //                //this customer (from cookie) should not be registered
                //                !customerByCookie.IsRegistered())
                //                user = customerByCookie;
                //        }
                //    }
                //}

                ////validation
                //if (!user.Deleted && user.Active && !user.RequireReLogin)
                //{
                //    SetCustomerCookie(user.UserGuid);
                //    _cachedCustomer = user;
                //}

                //return _cachedCustomer;
            }
            set
            {
                SetCustomerCookie(value.UserGuid);
                _cachedCustomer = value;
            }
        }




        /// <summary>
        /// Get or set current user working language
        /// </summary>
        //public virtual Language WorkingLanguage
        //{
        //    get
        //    {
        //        if (_cachedLanguage != null)
        //            return _cachedLanguage;

        //        Language detectedLanguage = null;
        //        if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
        //        {
        //            //get language from URL
        //            detectedLanguage = GetLanguageFromUrl();
        //        }
        //        if (detectedLanguage == null && _localizationSettings.AutomaticallyDetectLanguage)
        //        {
        //            //get language from browser settings
        //            //but we do it only once
        //            if (!this.CurrentCustomer.GetAttribute<bool>(SystemCustomerAttributeNames.LanguageAutomaticallyDetected, 
        //                _genericAttributeService, _storeContext.CurrentStore.Id))
        //            {
        //                detectedLanguage = GetLanguageFromBrowserSettings();
        //                if (detectedLanguage != null)
        //                {
        //                    _genericAttributeService.SaveAttribute(this.CurrentCustomer, SystemCustomerAttributeNames.LanguageAutomaticallyDetected,
        //                         true, _storeContext.CurrentStore.Id);
        //                }
        //            }
        //        }
        //        if (detectedLanguage != null)
        //        {
        //            //the language is detected. now we need to save it
        //            if (this.CurrentCustomer.GetAttribute<int>(SystemCustomerAttributeNames.LanguageId,
        //                _genericAttributeService, _storeContext.CurrentStore.Id) != detectedLanguage.Id)
        //            {
        //                _genericAttributeService.SaveAttribute(this.CurrentCustomer, SystemCustomerAttributeNames.LanguageId,
        //                    detectedLanguage.Id, _storeContext.CurrentStore.Id);
        //            }
        //        }

        //        var allLanguages = _languageService.GetAllLanguages(storeId: _storeContext.CurrentStore.Id);
        //        //find current customer language
        //        var languageId = this.CurrentCustomer.GetAttribute<int>(SystemCustomerAttributeNames.LanguageId,
        //            _genericAttributeService, _storeContext.CurrentStore.Id);
        //        var language = allLanguages.FirstOrDefault(x => x.Id == languageId);
        //        if (language == null)
        //        {
        //            //it not found, then let's load the default currency for the current language (if specified)
        //            languageId = _storeContext.CurrentStore.DefaultLanguageId;
        //            language = allLanguages.FirstOrDefault(x => x.Id == languageId);
        //        }
        //        if (language == null)
        //        {
        //            //it not specified, then return the first (filtered by current store) found one
        //            language = allLanguages.FirstOrDefault();
        //        }
        //        if (language == null)
        //        {
        //            //it not specified, then return the first found one
        //            language = _languageService.GetAllLanguages().FirstOrDefault();
        //        }

        //        //cache
        //        _cachedLanguage = language;
        //        return _cachedLanguage;
        //    }
        //    set
        //    {
        //        var languageId = value != null ? value.Id : 0;
        //        _genericAttributeService.SaveAttribute(this.CurrentCustomer,
        //            SystemCustomerAttributeNames.LanguageId,
        //            languageId, _storeContext.CurrentStore.Id);

        //        //reset cache
        //        _cachedLanguage = null;
        //    }
        //}



        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion
    }
}
