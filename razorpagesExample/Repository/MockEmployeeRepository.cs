using razorpagesExample.Models;

namespace razorpagesExample.Repository;

public class MockEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;
    public MockEmployeeRepository()
    {
        _employeeList = new List<Employee>()
        {
            new Employee {Id = 1, Name = "Ataberk Kaya", Email = "taberkkaya@gmail.com", Photo = "1.jpg", Department = "IT"},
            new Employee {Id = 2, Name = "Employee#2", Email = "taberkkaya@gmail.com", Photo = "2.jpg", Department = "HR"},
            new Employee {Id = 3, Name = "Employee#3", Email = "taberkkaya@gmail.com", Photo = "3.jpg", Department = "PM"},
            new Employee {Id = 4, Name = "Employee#4", Email = "taberkkaya@gmail.com", Photo = "4.jpg", Department = "SECURITY"},
            new Employee {Id = 5, Name = "Ataberk Kaya", Email = "taberkkaya@gmail.com", Photo = "1.jpg", Department = "IT"},
            new Employee {Id = 6, Name = "Employee#2", Email = "taberkkaya@gmail.com", Photo = "2.jpg", Department = "HR"},
            new Employee {Id = 7, Name = "Employee#3", Email = "taberkkaya@gmail.com", Photo = "3.jpg", Department = "PM"},
            new Employee {Id = 8, Name = "Employee#4", Email = "taberkkaya@gmail.com", Photo = "4.jpg", Department = "SECURITY"},
        };
    }
    public IEnumerable<Employee> GetAll()
    {
        return _employeeList;
    }

    public Employee GetById(int id)
    {
        return _employeeList.FirstOrDefault(x => x.Id == id);
    }

    public Employee Update(Employee entity)
    {
        Employee employee = _employeeList.FirstOrDefault(x => x.Id == entity.Id);

        if (employee != null)
        {
            employee.Name = entity.Name;
            employee.Email = entity.Email;
            employee.Photo = entity.Photo;
            employee.Department = entity.Department;
        }

        return employee;
    }
}