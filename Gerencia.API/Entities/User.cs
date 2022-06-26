namespace Gerencia.API.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationTime { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}