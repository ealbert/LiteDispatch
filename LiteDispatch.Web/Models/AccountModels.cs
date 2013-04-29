namespace LiteDispatch.Web.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Web.Mvc;

  public class UsersContext : DbContext
  {
    public UsersContext()
      : base("LiteDispatch.Security")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
  }

  [Table("UserProfile")]
  public class UserProfile
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
  }

  public class RegisterExternalLoginModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    public string ExternalLoginData { get; set; }
  }

  public class LocalPasswordModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm New Password")]
    [Compare("NewPassword", ErrorMessage = "The new and confirmation password fields do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginModel
  {
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }

  public class RegisterModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The new and confirmation password fields do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class ExternalLogin
  {
    public string Provider { get; set; }
    public string ProviderDisplayName { get; set; }
    public string ProviderUserId { get; set; }
  }
}
