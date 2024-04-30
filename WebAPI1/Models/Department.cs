using System.ComponentModel.DataAnnotations;

namespace WebAPI1.Models
{
    public class Department
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ?Name { get; set; }
        [Required]
        public string ?Manager { get; set; }
        public List<Employee> ?Employee { get; set; }
    }
}
