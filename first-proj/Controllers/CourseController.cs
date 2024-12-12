
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
    public IActionResult List()
    {
        var courses = new List<Course>(){
            new Course(){ Id = 1, Title = "ASP.NET Core Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_1.png" },
            new Course(){ Id = 2, Title = ".NET Core Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_2.png" },
            new Course(){ Id = 3, Title = "C# Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_3.png" },
        };
        return View("CourseList", courses);
    }
}