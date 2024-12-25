using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostController : Controller
{
    private IPostRepository _postRepository;
    private ICommentRepository _commentRepository;
    public PostController(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;

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

    [HttpPost]
    public JsonResult AddComment(int postId, string userName, string text)
    {
        var comment = new Comment
        {
            PostId = postId,
            User = new User
            {
                UserName = userName,
                Image = "dummy-user.jpeg"
            },
            Text = text,
            PublishedOn = DateTime.Now,
        };
        _commentRepository.CreateComment(comment);

        return Json(new
        {
            userName,
            text,
            comment.PublishedOn,
            comment.User.Image
        });
    }
}