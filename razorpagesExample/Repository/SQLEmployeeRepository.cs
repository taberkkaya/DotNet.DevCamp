using razorpagesExample.Models;

namespace razorpagesExample.Repository;

public class SQLEmployeeRepository : IEmployeeRepository
{
    private readonly DataContex _context;
    public SQLEmployeeRepository(DataContex contex)
    {
        _context = contex;
    }
    public IEnumerable<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public Employee GetById(int id)
    {
        return _context.Employees.FirstOrDefault(i => i.Id == id);
    }

    public Employee Update(Employee entity)
    {
        var entityToUpdate = _context.Employees.FirstOrDefault(i => i.Id == entity.Id);

        if (entityToUpdate != null)
        {
            entityToUpdate.Name = entity.Name;
            entityToUpdate.Email = entity.Email;
            entityToUpdate.Photo = entity.Photo;
            entityToUpdate.Department = entity.Department;
            _context.SaveChanges();
        }

        return entityToUpdate;
    }
}