using Manero_Backoffice.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero_Backoffice.Tests.Mocks
{
    public class MockUserManager : UserManager<ApplicationUser>
    {
        public IdentityResult Result { get; set; } = IdentityResult.Success;

        public MockUserManager()
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   Array.Empty<IUserValidator<ApplicationUser>>(),
                   Array.Empty<IPasswordValidator<ApplicationUser>>(),
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return Task.FromResult(Result);
        }

        public override IQueryable<ApplicationUser> Users => new List<ApplicationUser>().AsQueryable();

        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(new ApplicationUser { Email = email });
        }

        public override Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

    }
}
