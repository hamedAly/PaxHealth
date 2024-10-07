using Microsoft.AspNetCore.Mvc;
using PH.Web.Models;

namespace PH.Web.Controllers;

public class MedicalDataController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

