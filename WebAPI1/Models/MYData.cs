using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI1.Models
{
    public class MYData:IdentityDbContext<ApplicationUser>
    {
        public MYData()
        {
        }
        public MYData(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
