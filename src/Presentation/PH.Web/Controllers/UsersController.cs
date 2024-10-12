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

    [HttpGet]
    public IActionResult List() => View(_userService.List());
    [HttpGet]
    public IActionResult Create() => View();
    [HttpGet]
    public IActionResult Login() => View();

    
    
    [HttpPost]
    public async Task<IActionResult> Create(UserModel user)
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
            RedirectToAction(nameof(List));  
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel login)
    {
        return
            RedirectToAction("index", "MedicalData");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var user = await _userService.Find(id);
        var model = new UserModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Password = string.Empty
        };
        return
            View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UserModel user)
    {
        if (!ModelState.IsValid)
            return View(user);

        var userToUpdate = await _userService.Find(user.Id);
        userToUpdate.UserName = user.UserName;
        userToUpdate.Email = user.Email;
        var result = await _userService.Update(userToUpdate, user.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(List));
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        return View(user);
    }
}