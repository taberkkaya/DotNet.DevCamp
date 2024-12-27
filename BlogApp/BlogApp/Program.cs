using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlite(connectionString);
    // var version = new MySqlServerVersion(new Version(8, 0, 40));
    // options.UseMySql(connectionString, version);
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => options.LoginPath = "/User/Login"
);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.AddTestData(app);

app.MapControllerRoute(
    name: "post_details",
    pattern: "post/details/{url}",
    defaults: new { controller = "Post", action = "Details" }
);

app.MapControllerRoute(
    name: "post_details",
    pattern: "profile/{userName}",
    defaults: new { controller = "User", action = "Profile" }
);

app.MapControllerRoute(
    name: "post_by_tag",
    pattern: "post/tag/{tag}",
    defaults: new { controller = "Post", action = "Index" }
);

// app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
