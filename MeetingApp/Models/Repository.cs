namespace MeetingApp.Models;

public static class Repository
{
    private static List<UserInfo> _users = new();
    static Repository()
    {
        _users.Add(new UserInfo()
        {
            Id = 1,
            Name = "Ataberk",
            Phone = "+90 553 999 99 99",
            Email = "taberkkaya@gmail.com",
            WillAttend = true,
        });
        _users.Add(new UserInfo()
        {
            Id = 2,
            Name = "Ata",
            Phone = "+90 553 777 77 77",
            Email = "taberkkaya2@gmail.com",
            WillAttend = true,
        });
        _users.Add(new UserInfo()
        {
            Id = 3,
            Name = "Berk",
            Phone = "+90 553 666 66 66",
            Email = "taberkkaya3@gmail.com",
            WillAttend = false,
        });
    }
    public static List<UserInfo> Users
    {
        get
        {
            return _users;
        }
    }

    public static void CreateUser(UserInfo user)
    {
        user.Id = Users.Count + 1;
        _users.Add(user);
    }

    public static UserInfo? GetById(int id)
    {
        return Users.FirstOrDefault(user => user.Id == id);
    }
}