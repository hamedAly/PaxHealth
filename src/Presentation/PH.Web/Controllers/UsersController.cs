using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PH.Services;
using PH.Web.Models;

namespace PH.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult List()
    {
        return View(_userService.List());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserModel user)
    {
        if (!ModelState.IsValid)
            return View(user);

        var identityResult = await _userService.Create(user.UserName, user.Password, user.Email);
        if (identityResult.Errors.Any())
        {
            foreach (var error in identityResult.Errors)
                ModelState.AddModelError("", error.Description);
            return View(user);
        }

        return
            RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel login)
    {
        return
            RedirectToAction("index", "MedicalData");
    }
}