using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backoffice.Services;

public class RoleService(RoleManager<IdentityRole> roleManager, ILogger<RoleService> logger)
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly ILogger<RoleService> _logger = logger;
    //private readonly UserManager<IdentityUser> _userManager = userManager;


    public async Task<List<string>> GetRoleNamesAsync()
    {
        try
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (roles != null)
            {
                _logger.LogInformation("Roles retrieved.");
                return roles!;
            }
            else
            {
                _logger.LogWarning("No roles found.");
                return null!;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : RoleService.GetRoleNamesAsync() :: {ex.Message}");
            return null!;
        }
    }



    //public async Task<string> GetRoleNameByUserId(string id)
    //{
    //    try
    //    {
    //        var user = await _userManager.FindByIdAsync(id);
    //        if (user != null)
    //        {
    //            var roles = await _userManager.GetRolesAsync(user);
    //            if (roles.Count > 0)
    //            {
    //                _logger.LogInformation("Role retrieved for user.");
    //                return roles[0];
    //            }
    //            else
    //            {
    //                _logger.LogWarning("No roles found for user.");
    //                return null!;
    //            }
    //        }
    //        else
    //        {
    //            _logger.LogWarning("User not found.");
    //            return null!;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"ERROR : RoleService.GetRoleNameByUserId() :: {ex.Message}");
    //        return null!;
    //    }
    //}


}
