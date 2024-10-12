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

    public async Task<IdentityUser> Find(string userId)
    {
        return
            await userManager.FindByIdAsync(userId);
    }

    public async Task<IdentityResult> Update(IdentityUser user, string newPassword)
    {
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return result;
        await userManager.RemovePasswordAsync(user);
        await userManager.AddPasswordAsync(user, newPassword);
        return result;
    }
}