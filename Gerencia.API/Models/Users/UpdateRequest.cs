namespace Gerencia.API.Models.Users;

public class UpdateRequest
{
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}