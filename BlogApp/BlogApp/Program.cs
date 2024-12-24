using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlite(connectionString);
    // var version = new MySqlServerVersion(new Version(8, 0, 40));
    // options.UseMySql(connectionString, version);
});

var app = builder.Build();

SeedData.AddTestData(app);

app.MapGet("/", () => "Hello World!");

app.Run();
