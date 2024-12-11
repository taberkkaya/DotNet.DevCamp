
using Microsoft.AspNetCore.Mvc;

namespace first_proj.Controllers;

public class CourseController : Controller
{
    public string Index()
    {
        return "Course/Index";
    }
    public string List()
    {
        return "Course/List";
    }
}