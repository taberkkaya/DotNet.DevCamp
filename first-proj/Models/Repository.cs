namespace first_proj.Models;

public class Repository
{
    private static readonly List<Course> _courses = new();

    static Repository()
    {
        _courses = new List<Course>(){
            new Course(){ Id = 1, Title = "ASP.NET Core Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_1.png" },
            new Course(){ Id = 2, Title = ".NET Core Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_2.png" },
            new Course(){ Id = 3, Title = "C# Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_3.png" },
            new Course(){ Id = 4, Title = "ASP.NET Course",Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",Image="course_1.png" },
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