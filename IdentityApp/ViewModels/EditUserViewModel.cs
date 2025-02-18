using System.ComponentModel.DataAnnotations;
using IdentityApp.Models;

namespace IdentityApp.ViewModels;

public class EditUserViewModel
{
    public string? Id { get; set; }
    public string? FullName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }

    public IList<string>? SelectedRoles { get; set; }
}