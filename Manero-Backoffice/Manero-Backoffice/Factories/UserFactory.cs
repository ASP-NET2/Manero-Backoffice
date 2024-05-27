using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Data;

namespace Manero_Backoffice.Factories;

public class UserFactory
{
    public static ApplicationUser Create(RegistrationForm form)
    {
        return new ApplicationUser
        {
            Email = form.Email,
            UserName = form.Email
        };
    }

}
