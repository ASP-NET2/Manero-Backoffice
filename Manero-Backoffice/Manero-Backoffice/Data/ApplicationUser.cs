using Microsoft.AspNetCore.Identity;

namespace Manero_Backoffice.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? AddressId { get; set; }
        public AddressEntity? Address { get; set; } = null!;
    }
}
