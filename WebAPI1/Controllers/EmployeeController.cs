using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI1.DTO;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly MYData context;
        public EmployeeController(MYData context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult Getall()
        {
            List<Employee> ?list = context.Employees.Include(s=>s.Department).ToList();
            return  Ok(list);
        }
        [HttpGet("id",Name ="oneemployeeroute")]
        public ActionResult Get(int id) 
        {
            Employee ?Emp=context.Employees.Include(s=>s.Department).FirstOrDefault(e=>e.Id==id);
            EmpwithDeptDto EmpDto = new EmpwithDeptDto();
            EmpDto.DepartmentName = Emp.Department.Name;
            EmpDto.StudentName = Emp.Name;
            EmpDto.ID = Emp.Id;
            EmpDto.Address = Emp.Address;
            return Ok(EmpDto);
        }
    }
}
