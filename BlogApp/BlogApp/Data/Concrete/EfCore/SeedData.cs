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
                    new Tag { Text = "Web Programlama", Url = "web-programlama", Color = TagColors.success },
                    new Tag { Text = "Frontend", Url = "frontend", Color = TagColors.primary },
                    new Tag { Text = "Full Stack", Url = "fullstack", Color = TagColors.danger },
                    new Tag { Text = "PHP", Url = "php", Color = TagColors.warning }
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
                        Url = "asp-net-core",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId = 1,
                        Image = "dummy-image.jpg",
                        Comments = new List<Comment>
                        {
                            new Comment{ Text = "Süper kurs.", PublishedOn = DateTime.Now.AddDays(-20), UserId = 2},
                            new Comment{ Text = "Katılıyorum.", PublishedOn = DateTime.Now, UserId = 2},
                        }
                    },
                    new Post
                    {
                        Title = "PHP",
                        Content = "PHP Dersleri",
                        Url = "php",
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
                        Url = "django",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-25),
                        Tags = context.Tags.Take(4).ToList(),
                        UserId = 2,
                        Image = "dummy-image.jpg"
                    },
                    new Post
                    {
                        Title = "React",
                        Content = "React Dersleri",
                        Url = "react",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-50),
                        Tags = context.Tags.Take(4).ToList(),
                        UserId = 2,
                        Image = "dummy-image.jpg"
                    },
                    new Post
                    {
                        Title = "Angular",
                        Content = "Angular Dersleri",
                        Url = "angular",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-40),
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