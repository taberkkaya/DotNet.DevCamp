using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers;

public class MeetingController : Controller
{
    [HttpGet]
    public IActionResult Apply()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Apply(UserInfo userInfo)
    {
        if (ModelState.IsValid)
        {
            Repository.CreateUser(userInfo);
            ViewBag.UserCount = Repository.Users.Where(info => info.WillAttend == true).Count();
            return View("Thanks", userInfo);
        }
        else
        {
            return View(userInfo);
        }
    }

    [HttpGet]
    public IActionResult List()
    {
        return View(Repository.Users);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        return View(Repository.GetById(id));
    }
}