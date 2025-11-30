using App.Application.DTOs;
using App.Domain.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserDTO>>> GetAll();
        Task<Result<UserDTO>> GetByEmailAsync(string email);
        Task<Result> DeactivateAsync(Guid id);
        Task<Result> AddUserAsync(CreateUserDto createUserDto);
    }
}
