namespace BlogApp.Controllers;

using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Xml.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class UserController : Controller
{
    private IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);

            if (user == null)
            {
                var newUser = new User
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Image = "dummy-user.jpeg"
                };
                _userRepository.CreateUser(newUser);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Email veya kullanıcı adı kullanımda");
            }
        }

        return View(model);

    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Post");
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (isUser != null)
            {
                var userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? "dummy-user.jpeg"));

                if (isUser.Email == "admin@gmail.com")
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                }

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Post");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
            }
        }

        return View(model);
    }

    public IActionResult Profile(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return NotFound();
        }

        var user = _userRepository.Users
        .Include(x => x.Posts)
        .Include(x => x.Comments)
        .ThenInclude(x => x.Post)
        .FirstOrDefault(x => x.UserName == userName);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }


}