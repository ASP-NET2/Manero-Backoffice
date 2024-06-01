using Manero_Backoffice.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Manero_Backoffice.Tests.Mocks;

public class MockSignInManager : SignInManager<ApplicationUser>
{
    public MockSignInManager()
        : base(new MockUserManager(),
               new Mock<IHttpContextAccessor>().Object,
               new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
               new Mock<IOptions<IdentityOptions>>().Object,
               new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
               new Mock<IAuthenticationSchemeProvider>().Object,
               new Mock<IUserConfirmation<ApplicationUser>>().Object)
    { }

    public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return Task.FromResult(SignInResult.Success);
    }
}
