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
using static System.Net.Mime.MediaTypeNames;

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
                PhoneNumber = u.PhoneNumber,
                IsActive = u.IsActive
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

        public async Task<Result<UserDTO>> AddUserAsync(AddUserDTO dto)
        {
            // Check email duplication
            var existingUser = await _manager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return Result.Fail<UserDTO>(AppResponses.BadRequestResponse("Email already exists."));
            }

            var newUser = new User
            {
                UserName = dto.Email,
                Name = dto.Name,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            var createUserResult = await _manager.CreateAsync(newUser, dto.Password);

            if (!createUserResult.Succeeded)
            {
                return Result.Fail<UserDTO>(AppResponses.BadRequestResponse(
                    string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                ));
            }

            await _manager.AddToRoleAsync(newUser, "admin");
            var userDto = new UserDTO
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Address = newUser.Address,
                PhoneNumber = newUser.PhoneNumber
            };

            return Result.Success(userDto);
        }


    }
}
