using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
 
namespace PH.Web.Models;

public class CreateUserModel 
{
    public int Id { get; set; }

    [BindProperty]
    public required string UserName { get; set; }

    [BindProperty ]
    public required string Password { get; set; }

    [BindProperty, EmailAddress]
    public required string Email { get; set; }
 
}