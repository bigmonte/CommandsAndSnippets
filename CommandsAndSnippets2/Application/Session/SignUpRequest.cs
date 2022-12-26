using System.ComponentModel.DataAnnotations;
using  CommandsAndSnippets2.Application.Users;
using Microsoft.AspNetCore.Identity;

namespace   CommandsAndSnippets2.Users;

/// <summary>
/// Data used for the signup. 
/// </summary>
public class SignUpRequest
{
    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Required]
    [ProtectedPersonalData]
    public string FirstName { get; set; }
    
    [Required]
    [ProtectedPersonalData]
    public string LastName { get; set; }
    

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    /// <summary>
    /// Username 
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// ConfirmPassword
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    

    /// <summary>
    /// Convert RegisterViewModel to ApplicationUser
    /// </summary>
    /// <returns></returns>
    public ApplicationUser ToUser()
    {
        var newUser = new ApplicationUser();
        newUser.Email = Email;
        newUser.PasswordHash = Password;
        newUser.UserName = Username;
        //newUser.PhoneNumber = PhoneNumber;
        newUser.FirstName = FirstName;
        newUser.LastName = LastName;
        return newUser;
    }
}