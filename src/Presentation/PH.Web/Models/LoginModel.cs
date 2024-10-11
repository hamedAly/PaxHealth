using Microsoft.AspNetCore.Mvc;

namespace PH.Web.Models;

public class LoginModel  
{
    [BindProperty]
    public string UserName { get; set; }
    [BindProperty]
    public string Password { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }
}