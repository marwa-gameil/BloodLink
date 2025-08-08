using Microsoft.AspNetCore.Identity;
using App.Domain.Abstractions;


namespace App.Domain.Models;

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; }
}
