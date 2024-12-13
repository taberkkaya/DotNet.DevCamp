
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
        course.Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.";
        course.Image = "course_1.png";
        return View(course);
    }

    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            // return Redirect("/course/list");
            return RedirectToAction("List", "Course");
        }

        var course = Repository.GetById(id);
        return View(course);
    }

    public IActionResult List()
    {
        return View("CourseList", Repository.Courses);
    }
}