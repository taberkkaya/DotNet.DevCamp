using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostController : Controller
{
    private IPostRepository _postRepository;
    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;

    }
    public async Task<IActionResult> Index(string tag)
    {
        var posts = _postRepository.Posts;

        if (!string.IsNullOrEmpty(tag))
        {
            posts = posts.Include(x => x.Tags).Where(x => x.Tags.Any(t => t.Url == tag));
        }

        return View(new PostsViewModel
        {
            Posts = await posts.Include(x => x.Tags).ToListAsync()
        });
    }

    public async Task<IActionResult> Details(string? url)
    {
        return View(await _postRepository.Posts
        .Include(x => x.Tags)
        .Include(c => c.Comments)
        .ThenInclude(u => u.User)
        .FirstOrDefaultAsync(p => p.Url == url));
    }

    public IActionResult AddComment(int PostId, string UserName, string Text)
    {
        return View();

    }
}