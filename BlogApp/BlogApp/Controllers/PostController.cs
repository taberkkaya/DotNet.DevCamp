using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class PostController : Controller
{
    private IPostRepository _postRepository;
    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;

    }
    public IActionResult Index()
    {
        return View(new PostsViewModel
        {
            Posts = _postRepository.Posts.ToList(),

        });
    }
}