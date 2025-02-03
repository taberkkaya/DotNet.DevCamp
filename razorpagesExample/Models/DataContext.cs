using Microsoft.EntityFrameworkCore;

namespace razorpagesExample.Models;

public class DataContex : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=AKAYA;Database=RazorDb;Trusted_Connection=True;TrustServerCertificate=True");
    }
}