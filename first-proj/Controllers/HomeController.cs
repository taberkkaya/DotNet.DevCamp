using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using first_proj.Models;

namespace first_proj.Controllers;

public class HomeController : Controller
{

    public string Index()
    {
        return "Home/Index";
    }
    public string Contact()
    {
        return "Home/Contact";
    }
}
