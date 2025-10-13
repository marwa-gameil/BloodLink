using Microsoft.AspNetCore.Identity;
using App.Domain.Abstractions;


namespace App.Domain.Models;

public class User : IdentityUser<Guid>
{
    public int Id  { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
