using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Models
{
    /// <summary>
    /// The default implementation of <see cref="IdentityUser{TKey}"/> which uses a string as a primary key.
    /// Some properties were overridden for readability.  
    /// </summary>
    public  class User : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="User"/>.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        protected User(string email) 
        {
            Email = email;
        }
        
        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        [Key] [Required] [PersonalData]
        public override string Id { get; set; }

        /// <summary>
        /// First name and Last Name concatenated. 
        /// </summary>
        [Required] [MaxLength(55)]
        public override string UserName { get; set; }

        /// <summary>
        /// If the email was confirmed by the user or not. 
        /// </summary>
        public override bool EmailConfirmed { get; set; }

        [ProtectedPersonalData] [Required] [MaxLength(100)]
        public sealed override string Email { get; set; }
        
        [Required]
        public override string PasswordHash { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>
        /// A value in the past means the user is not locked out.
        /// </remarks>
        public override DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        /// <value>True if the user could be locked out, otherwise false.</value>
        public override bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        public override int AccessFailedCount { get; set; }
    }
}