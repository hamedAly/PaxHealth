using Microsoft.AspNetCore.Identity;

namespace PH.Services;

public class UserService(UserManager<IdentityUser> userManager) : IUserService
{
    public IEnumerable<IdentityUser> List()
    {
        return
            userManager.Users.ToList();
    }

    public async Task<IdentityResult> Create(string username, string password, string email)
    {
        var userIdentity = new IdentityUser { UserName = username, Email = email };
        return 
             await userManager.CreateAsync(userIdentity);
        
    }
}