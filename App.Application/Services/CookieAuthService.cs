using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Specifications;
/*{
namespace App.Application.Services
    public class CookieAuthService : ICookieAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<Charity> _charities;
        
        public CookieAuthService(UserManager<User> manager,SignInManager<User> signInManager,IRepository<Charity> charities)
        {
            _userManager = manager;
            _signInManager = signInManager;
            _charities = charities;
        }
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            User? user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
                if (result.Succeeded)
                {
                    Charity? charity = await _charities.GetOne(new Specification<Charity>(c => c.UserId == user.Id));
                    return Result.Success(
                        new UserDTO
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Address = user.Address,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            IsCharity = (charity != null)
                        }
                        );
                }
            }
            
            return Result.Fail<UserDTO>(AppResponses.UnAuthorizedResponse);
        }

        public async Task<Result> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Result.Success();
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDto registerDto)
        {
            var user = registerDto.ToUserModel();
         var result =   await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result.Fail<UserDTO>(AppResponses.BadRequestResponse( $"User creation failed: {errors}"));
            }
            if (registerDto.IsCharity)
            {
                string fileName = $"Charity_{user.Name}_{Guid.NewGuid()}.pdf";

                var folderPath = Path.Combine("wwwroot", "docs");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await registerDto.VerificationDoc.CopyToAsync(stream);
                }
                Charity charity = new Charity
                {
                    UserId = user.Id,
                    VerificationDocument= fileName,
                    IsVerfied=false
                };
                await _charities.Add(charity);
                await _charities.Save();
                await _userManager.AddToRoleAsync(user, "Charity");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Donor");
            }
            return Result.Success( user.ToUserDTO());

        }
    }
}
  */  