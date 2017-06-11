using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Domain.Users;
using Nop.Services.Events;

namespace Nop.Services.Users
{
    /// <summary>
    /// User attribute service
    /// </summary>
    public partial class UserAttributeService : IUserAttributeService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string USERATTRIBUTES_ALL_KEY = "Nop.userattribute.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user attribute ID
        /// </remarks>
        private const string USERATTRIBUTES_BY_ID_KEY = "Nop.userattribute.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user attribute ID
        /// </remarks>
        private const string USERATTRIBUTEVALUES_ALL_KEY = "Nop.userattributevalue.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user attribute value ID
        /// </remarks>
        private const string USERATTRIBUTEVALUES_BY_ID_KEY = "Nop.userattributevalue.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string USERATTRIBUTES_PATTERN_KEY = "Nop.userattribute.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string USERATTRIBUTEVALUES_PATTERN_KEY = "Nop.userattributevalue.";
        #endregion
        
        #region Fields

        private readonly IRepository<UserAttribute> _userAttributeRepository;
        private readonly IRepository<UserAttributeValue> _userAttributeValueRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="userAttributeRepository">User attribute repository</param>
        /// <param name="userAttributeValueRepository">User attribute value repository</param>
        /// <param name="eventPublisher">Event published</param>
        public UserAttributeService(ICacheManager cacheManager,
            IRepository<UserAttribute> userAttributeRepository,
            IRepository<UserAttributeValue> userAttributeValueRepository ,
            IEventPublisher eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._userAttributeRepository = userAttributeRepository;
            this._userAttributeValueRepository = userAttributeValueRepository;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        public virtual void DeleteUserAttribute(UserAttribute userAttribute)
        {
            if (userAttribute == null)
                throw new ArgumentNullException("userAttribute");

            _userAttributeRepository.Delete(userAttribute);

            _cacheManager.RemoveByPattern(USERATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(USERATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityDeleted(userAttribute);
        }

        /// <summary>
        /// Gets all user attributes
        /// </summary>
        /// <returns>User attributes</returns>
        public virtual IList<UserAttribute> GetAllUserAttributes()
        {
            string key = USERATTRIBUTES_ALL_KEY;
            return _cacheManager.Get(key, () =>
            {
                var query = from ca in _userAttributeRepository.Table
                            orderby ca.DisplayOrder, ca.Id
                            select ca;
                return query.ToList();
            });
        }

        /// <summary>
        /// Gets a user attribute 
        /// </summary>
        /// <param name="userAttributeId">User attribute identifier</param>
        /// <returns>User attribute</returns>
        public virtual UserAttribute GetUserAttributeById(int userAttributeId)
        {
            if (userAttributeId == 0)
                return null;

            string key = string.Format(USERATTRIBUTES_BY_ID_KEY, userAttributeId);
            return _cacheManager.Get(key, () => _userAttributeRepository.GetById(userAttributeId));
        }

        /// <summary>
        /// Inserts a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        public virtual void InsertUserAttribute(UserAttribute userAttribute)
        {
            if (userAttribute == null)
                throw new ArgumentNullException("userAttribute");

            _userAttributeRepository.Insert(userAttribute);

            _cacheManager.RemoveByPattern(USERATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(USERATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
           // _eventPublisher.EntityInserted(userAttribute);
        }

        /// <summary>
        /// Updates the user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        public virtual void UpdateUserAttribute(UserAttribute userAttribute)
        {
            if (userAttribute == null)
                throw new ArgumentNullException("userAttribute");

            _userAttributeRepository.Update(userAttribute);

            _cacheManager.RemoveByPattern(USERATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(USERATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityUpdated(userAttribute);
        }

        /// <summary>
        /// Deletes a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        public virtual void DeleteUserAttributeValue(UserAttributeValue userAttributeValue)
        {
            if (userAttributeValue == null)
                throw new ArgumentNullException("userAttributeValue");

            _userAttributeValueRepository.Delete(userAttributeValue);

            _cacheManager.RemoveByPattern(USERATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(USERATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityDeleted(userAttributeValue);
        }

        /// <summary>
        /// Gets user attribute values by user attribute identifier
        /// </summary>
        /// <param name="userAttributeId">The user attribute identifier</param>
        /// <returns>User attribute values</returns>
        public virtual IList<UserAttributeValue> GetUserAttributeValues(int userAttributeId)
        {
            string key = string.Format(USERATTRIBUTEVALUES_ALL_KEY, userAttributeId);
            return _cacheManager.Get(key, () =>
            {
                var query = from cav in _userAttributeValueRepository.Table
                            orderby cav.DisplayOrder, cav.Id
                            where cav.UserAttributeId == userAttributeId
                            select cav;
                var userAttributeValues = query.ToList();
                return userAttributeValues;
            });
        }
        
        /// <summary>
        /// Gets a user attribute value
        /// </summary>
        /// <param name="userAttributeValueId">User attribute value identifier</param>
        /// <returns>User attribute value</returns>
        public virtual UserAttributeValue GetUserAttributeValueById(int userAttributeValueId)
        {
            if (userAttributeValueId == 0)
                return null;

            string key = string.Format(USERATTRIBUTEVALUES_BY_ID_KEY, userAttributeValueId);
            return _cacheManager.Get(key, () => _userAttributeValueRepository.GetById(userAttributeValueId));
        }

        /// <summary>
        /// Inserts a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        public virtual void InsertUserAttributeValue(UserAttributeValue userAttributeValue)
        {
            if (userAttributeValue == null)
                throw new ArgumentNullException("userAttributeValue");

            _userAttributeValueRepository.Insert(userAttributeValue);

            _cacheManager.RemoveByPattern(USERATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(USERATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityInserted(userAttributeValue);
        }

        /// <summary>
        /// Updates the user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        public virtual void UpdateUserAttributeValue(UserAttributeValue userAttributeValue)
        {
            if (userAttributeValue == null)
                throw new ArgumentNullException("userAttributeValue");

            _userAttributeValueRepository.Update(userAttributeValue);

            _cacheManager.RemoveByPattern(USERATTRIBUTES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(USERATTRIBUTEVALUES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityUpdated(userAttributeValue);
        }
        
        #endregion
    }
}
