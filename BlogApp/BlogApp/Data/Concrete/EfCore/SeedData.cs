using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore;

public static class SeedData
{
    public static void AddTestData(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Text = "Web Programlama" },
                    new Tag { Text = "Frontend" },
                    new Tag { Text = "Full Stack" },
                    new Tag { Text = "PHP" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "admin", Image = "dummy-user.jpeg" },
                    new User { UserName = "taberkkaya", Image = "dummy-user.jpeg" }
                );
                context.SaveChanges();
            }

            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "ASP.NET Core",
                        Content = "ASP.NET Core Dersleri",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId = 1,
                        Image = "dummy-image.jpg"
                    },
                    new Post
                    {
                        Title = "PHP",
                        Content = "PHP Dersleri",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        UserId = 1,
                        Image = "dummy-image.jpg"
                    },
                    new Post
                    {
                        Title = "Django",
                        Content = "Django Dersleri",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        UserId = 2,
                        Image = "dummy-image.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}