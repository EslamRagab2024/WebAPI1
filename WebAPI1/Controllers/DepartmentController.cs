using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using WebAPI1.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI1.DTO;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly MYData context;

        public DepartmentController(MYData context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Department> ?departmentList = context.Departments.ToList();
            return Ok(departmentList);
        }
        [HttpGet("id", Name = "GetOneDeptRoute")]
        //[Route("id")]
        public IActionResult GetByID(int id)
        {
            Department ?dept = context.Departments.Include(e=>e.Employee)
                .FirstOrDefault(d=>d.Id == id);
            DeptwithEmpName? DepDto = new DeptwithEmpName();
            DepDto.ID = id;
            DepDto.DeptName = dept.Name;
            foreach (var item in dept.Employee)
            {
                DepDto.EmployeesName.Add(item.Name);
            }
            return Ok(DepDto);
        }
        [HttpGet]
        [Route("name")]
        public IActionResult GetByName(string name)
        {
            Department ?dept = context.Departments.FirstOrDefault(x => x.Name== name);
            if (dept != null)
                return Ok(dept);
            else
                return BadRequest(dept);
        }
        [HttpPost]
        public IActionResult PostResult(Department dept)
        {
            if(ModelState.IsValid)
            {
                context.Departments.Add(dept);
                context.SaveChanges();
                string ?url = Url.Link("GetOneDeptRoute", new { id = dept.Id });

                return Created(url, dept);
            }
            return BadRequest(ModelState);

        }
        [HttpPut]
        public ActionResult update( [FromRoute]int id ,[FromBody] Department dept)
        {
            if (ModelState.IsValid) 
            {
                Department ?old = context.Departments.FirstOrDefault(x => x.Id == id);
                if(old != null)
                {
                    old.Name=dept.Name;
                    old.Manager = dept.Manager;
                    context.SaveChanges();
                    return StatusCode(204, old);
                }
                else
                {
                   return BadRequest("id not valid");
                }
            }
            else
            { return BadRequest(ModelState); }
        }
        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            Department? department = context.Departments.FirstOrDefault(x=>x.Id == id);
            if (department != null) 
            {
                context.Departments.Remove(department);
                context.SaveChanges();
                return StatusCode(204, "remove success");
            }
            else
            { return BadRequest("ID not found"); }
        }
    }
}
