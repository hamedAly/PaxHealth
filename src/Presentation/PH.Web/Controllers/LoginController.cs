using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PH.Web.Models;

namespace PH.Web.Controllers;

public class LoginController : Controller
{
    private SignInManager<IdentityUser> signInManager;

    public LoginController(SignInManager<IdentityUser> signinMgr)
    {
        signInManager = signinMgr;
    }

    public async Task<IActionResult> OnPostAsync(LoginViewModel userClaims)
    {
        if (ModelState.IsValid)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(userClaims.UserName, userClaims.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(userClaims.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "Invalid username or password");
        }

        return View();
    }
}