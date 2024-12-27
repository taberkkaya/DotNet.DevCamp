using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostController : Controller
{
    private IPostRepository _postRepository;
    private ICommentRepository _commentRepository;
    private ITagRepository _tagRepository;
    public PostController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _tagRepository = tagRepository;

    }
    public async Task<IActionResult> Index(string tag)
    {
        var claims = User.Claims;
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
        .Include(x => x.User)
        .Include(x => x.Tags)
        .Include(c => c.Comments)
        .ThenInclude(u => u.User)
        .FirstOrDefaultAsync(p => p.Url == url));
    }

    [HttpPost]
    public JsonResult AddComment(int postId, string text)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var avatar = User.FindFirstValue(ClaimTypes.UserData);
        var comment = new Comment
        {
            PostId = postId,
            UserId = int.Parse(userId ?? ""),
            Text = text,
            PublishedOn = DateTime.Now,
        };
        _commentRepository.CreateComment(comment);

        return Json(new
        {
            userName,
            text,
            comment.PublishedOn,
            avatar
        });
    }

    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(PostCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _postRepository.CreatePost(new Post
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                Url = model.Url,
                UserId = int.Parse(user ?? ""),
                PublishedOn = DateTime.Now,
                Image = "dummy-user.jpeg",
                IsActive = false
            });
            return RedirectToAction("Index", "Post");

        }
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> List()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
        var role = User.FindFirstValue(ClaimTypes.Role);

        var posts = _postRepository.Posts;

        if (string.IsNullOrEmpty(role))
        {
            posts = posts.Where(x => x.UserId == userId);
        }

        return View(await posts.ToListAsync());
    }

    [Authorize]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = _postRepository.Posts.Include(t => t.Tags).FirstOrDefault(x => x.PostId == id);

        if (post == null)
        {
            return NotFound();
        }

        ViewBag.Tags = _tagRepository.Tags.ToList();

        return View(new PostCreateViewModel
        {
            PostId = post.PostId,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            IsActive = post.IsActive,
            Url = post.Url,
            Tags = post.Tags
        });
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(PostCreateViewModel post, int[] tagIds)
    {
        if (ModelState.IsValid)
        {
            var entityToUpdate = new Post
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                Description = post.Description,
                Url = post.Url,
            };

            if (User.FindFirstValue(ClaimTypes.Role) == "admin")
            {
                entityToUpdate.IsActive = post.IsActive;
            }

            _postRepository.EditPost(entityToUpdate, tagIds);
            return RedirectToAction("List");
        }

        return View();
    }
}