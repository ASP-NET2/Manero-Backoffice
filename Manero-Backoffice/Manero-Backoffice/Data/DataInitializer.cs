using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Manero_Backoffice.Data;

public class DataInitializer
{
    public static async Task EnsureRolesCreated(RoleManager<IdentityRole> roleManager)
    {

        string[] roles = ["Admin", "Super Admin"];

        try
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole(role);
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }

        catch { }
    }
}
