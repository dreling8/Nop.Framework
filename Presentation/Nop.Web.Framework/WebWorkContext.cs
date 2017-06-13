using System;
using System.Linq;
using System.Web;
using Nop.Core; 
using Nop.Core.Fakes;
using Nop.Domain.Localization;
using Nop.Domain.Users;
using Nop.Services.Authentication;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Users;
using Nop.Web.Framework.Localization;

namespace Nop.Web.Framework
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string UserCookieName = "Nop.user";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILanguageService _languageService;
        private readonly LocalizationSettings _localizationSettings;
        //private readonly IUserAgentHelper _userAgentHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private User _cachedUser;
        private Language _cachedLanguage;

        #endregion

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext,
             IUserService userService,
            IAuthenticationService authenticationService,
             //IUserAgentHelper userAgentHelper, 
             IGenericAttributeService genericAttributeService,
            ILanguageService languageService,
            LocalizationSettings localizationSettings
           
            )
        {
            this._httpContext = httpContext;
            this._userService = userService;
            this._authenticationService = authenticationService;
            //this._userAgentHelper = userAgentHelper;
            this._languageService = languageService;
            this._localizationSettings = localizationSettings;
            this._genericAttributeService = genericAttributeService;
            
        }

        #endregion

        #region Utilities

        protected virtual HttpCookie GetUserCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[UserCookieName];
        }

        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(UserCookieName);
                cookie.HttpOnly = true;
                cookie.Value = userGuid.ToString();
                if (userGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(UserCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        protected virtual Language GetLanguageFromUrl()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            string virtualPath = _httpContext.Request.AppRelativeCurrentExecutionFilePath;
            string applicationPath = _httpContext.Request.ApplicationPath;
            if (!virtualPath.IsLocalizedUrl(applicationPath, false))
                return null;

            var seoCode = virtualPath.GetLanguageSeoCodeFromUrl(applicationPath, false);
            if (String.IsNullOrEmpty(seoCode))
                return null;

            var language = _languageService
                .GetAllLanguages()
                .FirstOrDefault(l => seoCode.Equals(l.UniqueSeoCode, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published )
            {
                return language;
            }

            return null;
        }

        protected virtual Language GetLanguageFromBrowserSettings()
        {
            if (_httpContext == null ||
                _httpContext.Request == null ||
                _httpContext.Request.UserLanguages == null)
                return null;

            var userLanguage = _httpContext.Request.UserLanguages.FirstOrDefault();
            if (String.IsNullOrEmpty(userLanguage))
                return null;

            var language = _languageService
                .GetAllLanguages()
                .FirstOrDefault(l => userLanguage.Equals(l.LanguageCulture, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published )
            {
                return language;
            }

            return null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user
        /// </summary>25141240
        public virtual User CurrentUser
        {
            get
            {
                //return new User()
                //{ 
                //    Username = "Guest",
                //    UserGuid = Guid.NewGuid(),
                //    CreatedOnUtc =  DateTime.UtcNow,
                //    LastActivityDateUtc = DateTime.UtcNow,
                //};

                if (_cachedUser != null)
                    return _cachedUser;

                User user = null;

                //if (_httpContext == null || _httpContext is FakeHttpContext)
                //{
                //    //check whether request is made by a background task
                //    //in this case return built-in user record for background task
                //    user = _userService.GetUserBySystemName(SystemUserNames.BackgroundTask);
                //}

                ////check whether request is made by a search engine
                ////in this case return built-in user record for search engines 
                ////or comment the following two lines of code in order to disable this functionality
                //if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                //{
                //    if (_userAgentHelper.IsSearchEngine())
                //    {
                //        user = _userService.GetUserBySystemName(SystemUserNames.SearchEngine);
                //    }
                //}

                //registered user
                if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                {
                    user = _authenticationService.GetAuthenticatedUser();
                }

                //impersonate user if required (currently used for 'phone order' support)
                if (user != null && !user.Deleted && user.Active && !user.RequireReLogin)
                {
                    var impersonatedUserId = user.GetAttribute<int?>(SystemUserAttributeNames.ImpersonatedUserId);
                    if (impersonatedUserId.HasValue && impersonatedUserId.Value > 0)
                    {
                        var impersonatedUser = _userService.GetUserById(impersonatedUserId.Value);
                        if (impersonatedUser != null && !impersonatedUser.Deleted && impersonatedUser.Active && !impersonatedUser.RequireReLogin)
                        {
                            //set impersonated user 
                            user = impersonatedUser;
                        }
                    }
                }

                //load guest user
                if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                {
                    var userCookie = GetUserCookie();
                    if (userCookie != null && !String.IsNullOrEmpty(userCookie.Value))
                    {
                        Guid userGuid;
                        if (Guid.TryParse(userCookie.Value, out userGuid))
                        {
                            var userByCookie = _userService.GetUserByGuid(userGuid);
                            if (userByCookie != null &&
                                //this user (from cookie) should not be registered
                                !userByCookie.IsRegistered())
                                user = userByCookie;
                        }
                    }
                    //else
                    //{
                    //    user = _userService.GetUserBySystemName("Guest");
                    //}
                }

                //create guest if not exists
                if (user == null || user.Deleted || !user.Active || user.RequireReLogin)
                {
                    user = _userService.InsertGuestUser();
                }

                //validation
                if (!user.Deleted && user.Active && !user.RequireReLogin)
                {
                    SetUserCookie(user.UserGuid);
                    _cachedUser = user;
                }

                return _cachedUser;
            }
            set
            {
                SetUserCookie(value.UserGuid);
                _cachedUser = value;
            }
        }




        /// <summary>
        /// Get or set current user working language
        /// </summary>
        public virtual Language WorkingLanguage
        {
            get
            {
                if (_cachedLanguage != null)
                    return _cachedLanguage;

                Language detectedLanguage = null;
                if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                {
                    //get language from URL
                    detectedLanguage = GetLanguageFromUrl();
                }
                if (detectedLanguage == null && _localizationSettings.AutomaticallyDetectLanguage)
                {
                    //get language from browser settings
                    //but we do it only once
                    if (!this.CurrentUser.GetAttribute<bool>(SystemUserAttributeNames.LanguageAutomaticallyDetected,
                        _genericAttributeService))
                    {
                        detectedLanguage = GetLanguageFromBrowserSettings();
                        if (detectedLanguage != null)
                        {
                            _genericAttributeService.SaveAttribute(this.CurrentUser, SystemUserAttributeNames.LanguageAutomaticallyDetected,
                                 true);
                        }
                    }
                }
                if (detectedLanguage != null)
                {
                    //the language is detected. now we need to save it
                    if (this.CurrentUser.GetAttribute<int>(SystemUserAttributeNames.LanguageId,
                        _genericAttributeService) != detectedLanguage.Id)
                    {
                        _genericAttributeService.SaveAttribute(this.CurrentUser, SystemUserAttributeNames.LanguageId,
                            detectedLanguage.Id);
                    }
                }

                var allLanguages = _languageService.GetAllLanguages();
                //find current user language
                var languageId = this.CurrentUser.GetAttribute<int>(SystemUserAttributeNames.LanguageId,
                    _genericAttributeService);
                var language = allLanguages.FirstOrDefault(x => x.Id == languageId);
                if (language == null)
                {
                    //it not found, then let's load the default currency for the current language (if specified)
                    //languageId = _storeContext.CurrentStore.DefaultLanguageId;
                    language = allLanguages.FirstOrDefault(x => x.Id == 0);
                }
                if (language == null)
                {
                    //it not specified, then return the first (filtered by current store) found one
                    language = allLanguages.FirstOrDefault();
                }
                if (language == null)
                {
                    //it not specified, then return the first found one
                    language = _languageService.GetAllLanguages().FirstOrDefault();
                }

                //cache
                _cachedLanguage = language;
                return _cachedLanguage;
            }
            set
            {
                var languageId = value != null ? value.Id : 0;
                _genericAttributeService.SaveAttribute(this.CurrentUser,
                    SystemUserAttributeNames.LanguageId,
                    languageId);

                //reset cache
                _cachedLanguage = null;
            }
        }



        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion
    }
}
