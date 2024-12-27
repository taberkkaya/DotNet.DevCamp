using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore;

public class EfPostRepository : IPostRepository
{
    private readonly BlogContext _context;
    public EfPostRepository(BlogContext context)
    {
        _context = context;
    }
    public IQueryable<Post> Posts => _context.Posts;

    public void CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void EditPost(Post post)
    {
        var entity = _context.Posts.FirstOrDefault(x => x.PostId == post.PostId);

        if (entity != null)
        {
            entity.Title = post.Title;
            entity.Description = post.Description;
            entity.Url = post.Url;
            entity.Content = post.Content;
            entity.IsActive = post.IsActive;
            _context.Posts.Update(entity);
            _context.SaveChanges();
        }
    }

    public void EditPost(Post post, int[] tagIds)
    {
        var entity = _context.Posts.Include(t => t.Tags).FirstOrDefault(x => x.PostId == post.PostId);

        if (entity != null)
        {
            entity.Title = post.Title;
            entity.Description = post.Description;
            entity.Url = post.Url;
            entity.Content = post.Content;
            entity.IsActive = post.IsActive;
            entity.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();
            _context.Posts.Update(entity);
            _context.SaveChanges();
        }
    }
}