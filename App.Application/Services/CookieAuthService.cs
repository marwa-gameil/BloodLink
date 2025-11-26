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


namespace App.Application.Services;

public class CookieAuthService : ICookieAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public CookieAuthService(UserManager<User> manager, SignInManager<User> signInManager)
    {
        _userManager = manager;
        _signInManager = signInManager;
    }

    public async Task<Result> LoginAsync(LoginDTO loginDTO)
    {
        User? user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user is null)
            return Result.Fail(AppResponses.UnAuthorizedResponse);

        
        if (!user.IsActive)
            return Result.Fail(AppResponses.UnAuthorizedResponse);

        var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
        if (result.Succeeded)
            return Result.Success();

        return Result.Fail(AppResponses.UnAuthorizedResponse);
    }

    public async Task<Result> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return Result.Success();
    }
}