using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backoffice.Services;

public class RoleService(RoleManager<IdentityRole> roleManager, ILogger<RoleService> logger)
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly ILogger<RoleService> _logger = logger;

    public async Task<List<string>> GetRoleNamesAsync()
    {
        try
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if(roles != null)
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
}
