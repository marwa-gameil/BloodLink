using App.Application.DTOs;
using App.Application.Interfaces;
using App.Application.Utilities;
using App.Domain.Models;
using App.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> _manager;
        public UserService(UserManager<User> manager)
        {
            _manager = manager;
        }
        public async Task<Result> DeactivateAsync(Guid id)
        {
            User? user = await _manager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return Result.Fail(AppResponses.NotFoundResponse(id, nameof(User)));
            }
            user.IsActive = false;
            var result = await _manager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result.Fail(AppResponses.BadRequestResponse("Failed to deactivate user."));
            }
            return Result.Success();
        }

        public async Task<Result<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _manager.Users.ToListAsync();

            var usersDto = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber
            });

            return Result.Success(usersDto);
        }


        public async Task<Result<UserDTO>> GetByEmailAsync(string email)
        {
            User? user = await _manager.FindByEmailAsync(email);
            return user switch
            {
                null => Result.Fail<UserDTO>(AppResponses.NotFoundResponse(email, nameof(User))),
                _ => Result.Success(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber
                })
            };
        }
    }
}
