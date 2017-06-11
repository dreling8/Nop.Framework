using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Services.Events;

namespace Nop.Services.Users.Cache
{
    /// <summary>
    /// User cache event consumer (used for caching of current user password)
    /// </summary>
    public partial class UserCacheEventConsumer : IConsumer<UserPasswordChangedEvent>
    {
        #region Constants

        /// <summary>
        /// Key for current user password lifetime
        /// </summary>
        /// <remarks>
        /// {0} : user identifier
        /// </remarks>
        public const string CUSTOMER_PASSWORD_LIFETIME = "Nop.users.passwordlifetime-{0}";

        #endregion

        #region Fields

        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public UserCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            this._cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        }

        #endregion

        #region Methods

        //password changed
        public void HandleEvent(UserPasswordChangedEvent eventMessage)
        {
            _cacheManager.Remove(string.Format(CUSTOMER_PASSWORD_LIFETIME, eventMessage.Password.UserId));
        }

        #endregion
    }
}
