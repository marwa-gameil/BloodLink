using App.Domain.Models;


namespace App.Application.Interfaces
{
    public interface ICurrentLoggedInUser
    {
        string UserId { get; }
        Task<User> GetUser();
    }
}
