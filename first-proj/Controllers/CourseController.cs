
using Microsoft.AspNetCore.Mvc;

namespace first_proj.Controllers;

public class CourseController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult List()
    {
        return View("CourseList");
    }
}