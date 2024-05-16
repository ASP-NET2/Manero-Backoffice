using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Data;
using Manero_Backoffice.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backoffice.Services;

public class UserService(SignInManager<ApplicationUser> signInManager, ILogger<UserService> logger, UserManager<ApplicationUser> userManager)
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<UserResult> LoginAsync(LoginForm form)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, form.LockoutOnFailure);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return new UserResult { Succeeded = true };
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User account is locked out.");
                return new UserResult { Succeeded = false, ErrorMessage = "User account is locked out." };
            }
            else if (result.IsNotAllowed)
            {
                _logger.LogWarning("User is not allowed to sign in.");
                return new UserResult { Succeeded = false, ErrorMessage = "User is not allowed to sign in." };
            }
            else if (result.RequiresTwoFactor)
            {
                _logger.LogWarning("User requires two-factor authentication.");
                return new UserResult { Succeeded = false, ErrorMessage = "User requires two-factor authentication." };
            }
            else
            {
                _logger.LogWarning("Incorrect Email or Password.");
                return new UserResult { Succeeded = false, ErrorMessage = "Incorrect Email or Password." };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : UserService.LoginAsync() :: {ex.Message}");
            return new UserResult { Succeeded = false, ErrorMessage = "Something went wrong. Try again later." };
        }
    }

    public async Task<UserResult> CreateAsync(RegistrationForm form)
    {
        try
        {
            var result = await _userManager.CreateAsync(UserFactory.Create(form), form.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created");
                return new UserResult { Succeeded = true };
            }
            else if(await _userManager.Users.AnyAsync(x => x.Email == form.Email))
            {
                _logger.LogWarning("User already exists");
                return new UserResult { Succeeded = false, ErrorMessage = "User already exists." };
            }
            else
            {
                _logger.LogWarning("User creation failed");
                return new UserResult { Succeeded = false, ErrorMessage = result.Errors.ToList().FirstOrDefault()!.Description };
            }
                
        }

        catch (Exception ex)
        {
            _logger.LogError($"ERROR : UserService.CreateAsync() :: {ex.Message}");
        }

        return null!;
    }

    public async Task<UserResult> LoginAsync(ApplicationUser user, bool isPersistent)
    {
        try
        {
            await _signInManager.SignInAsync(user, isPersistent);
            _logger.LogInformation("User logged in.");
            return new UserResult { Succeeded = true };
        }

        catch (Exception ex)
        {
            _logger.LogError($"ERROR : UserService.LoginAsync() :: {ex.Message}");
            return new UserResult { Succeeded = false, ErrorMessage = "Something went wrong. Try again later." };
        }
        

    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {

        try
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user ?? null!;
        }

        catch (Exception ex)
        {
            _logger.LogError($"ERROR : UserService.FindByEmailAsync() :: {ex.Message}");
            return null!;
        }
    }
}
