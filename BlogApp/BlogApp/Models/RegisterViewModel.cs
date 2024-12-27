using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Kullanıcı Adı")]
    public string? UserName { get; set; }

    [Required]
    [Display(Name = "Adınız")]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Eposta")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Parola")]
    [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmiyor.")]
    public string? ConfirmPassword { get; set; }

}