using Nop.Domain.Users;

namespace Nop.Services.Users
{
    /// <summary>
    /// User logged-in event
    /// </summary>
    public class UserLoggedinEvent
    {
        public UserLoggedinEvent(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User
        {
            get; private set;
        }
    }
    /// <summary>
    /// "User is logged out" event
    /// </summary>
    public class UserLoggedOutEvent
    {
        public UserLoggedOutEvent(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// Get or set the user
        /// </summary>
        public User User { get; private set; }
    }

    /// <summary>
    /// User registered event
    /// </summary>
    public class UserRegisteredEvent
    {
        public UserRegisteredEvent(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User
        {
            get; private set;
        }
    }

    /// <summary>
    /// User password changed event
    /// </summary>
    public class UserPasswordChangedEvent
    {
        public UserPasswordChangedEvent(UserPassword password)
        {
            this.Password = password;
        }

        /// <summary>
        /// User password
        /// </summary>
        public UserPassword Password { get; private set; }
    }

}