
using first_proj.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_proj.Controllers;

public class CourseController : Controller
{
    public IActionResult Index()
    {
        var course = new Course();
        course.Id = 1;
        course.Title = "ASP.NET Core Course";

        return View(course);
    }
    public IActionResult List()
    {
        return View("CourseList");
    }
}