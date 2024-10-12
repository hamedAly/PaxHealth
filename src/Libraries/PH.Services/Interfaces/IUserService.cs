using Microsoft.AspNetCore.Identity;

namespace PH.Services;

public interface IUserService
{
    IEnumerable<IdentityUser> List();
    Task<IdentityResult> Create(string username, string password, string email);
}