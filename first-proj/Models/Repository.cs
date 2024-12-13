namespace first_proj.Models;

public class Repository
{
    private static readonly List<Course> _courses = new();

    static Repository()
    {
        _courses = new List<Course>(){
            new Course(){
                Id = 1,
                Title = "ASP.NET Core Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
                Image="course_1.png",
                Tags = new string[]{
                    "Test-1","Test-2"
                },
                isActive = true,
                isHome = true },

            new Course(){
                Id = 2,
                Title = ".NET Core Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
                Image="course_2.png",
                Tags = null,
                isActive = true,
                isHome = true },

            new Course(){
                Id = 3,
                Title = "C# Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
                Image="course_3.png",
                Tags = null,
                isActive = true,
                isHome = false },

            new Course(){
                Id = 4,
                Title = "ASP.NET Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
                Image="course_1.png",
                Tags = new string[]{
                    "Test-1","Test-2"
                },
                isActive = false,
                isHome = true },
        };
    }

    public static List<Course> Courses
    {
        get
        {
            return _courses;
        }
    }

    public static Course? GetById(int? id)
    {
        return _courses.FirstOrDefault(c => c.Id == id);
    }

}