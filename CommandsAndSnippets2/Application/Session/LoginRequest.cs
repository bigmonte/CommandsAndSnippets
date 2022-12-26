using System.ComponentModel.DataAnnotations;

namespace   CommandsAndSnippets2.Users
{
    // from: https://github.com/dotnet/aspnetcore/tree/main/src/Identity/samples/IdentitySample.Mvc/Models/AccountViewModels
    /// <summary>
    /// Login Request adata
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

