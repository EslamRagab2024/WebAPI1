namespace WebAPI1.DTO
{
    public class DeptwithEmpName
    {
        public int ID { get; set; }
        public string ?DeptName { get; set; }
        public List<string> EmployeesName { get; set; } = new List<string>();
    }
}

