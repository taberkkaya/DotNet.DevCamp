using MeetingApp.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        int hour = DateTime.Now.Hour;

        // ViewBag.SayHi = hour > 12 ? "İyi Günler" : "Günaydın";
        // ViewBag.Username = "Ataberk";

        ViewData["SayHi"] = hour > 12 ? "İyi Günler" : "Günaydın";
        ViewData["Username"] = "Ataberk";

        var meetingInfo = new MeetingInfo()
        {
            Id = 1,
            Location = "İstanbul, ABC Kongre Merkezi",
            Date = new DateTime(2024, 01, 20, 20, 0, 0),
            NumberOfPeople = 100
        };


        return View(meetingInfo);
    }
}