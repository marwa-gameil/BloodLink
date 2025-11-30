using Microsoft.AspNetCore.Identity;
using App.Domain.Abstractions;


namespace App.Web.Models;

public class User  : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string? Governorate { get; set; }
    public Hospital? Hospital { get; set; } 
    public BloodBank? BloodBank { get; set; } 
    public bool IsActive { get; set; } = true;

}
