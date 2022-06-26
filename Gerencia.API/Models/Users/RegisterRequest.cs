namespace Gerencia.API.Models.Users;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}