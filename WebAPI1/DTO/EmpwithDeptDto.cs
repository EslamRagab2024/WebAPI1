﻿namespace WebAPI1.DTO
{
    //Hide Model SEcure Model Structure & Solve Serializtion Propble CYCLE
    public class EmpwithDeptDto
    {
        public int ID { get; set; }
        public string ?StudentName { get; set; }
        public string ?Address { get; set; }
        public string? DepartmentName { get; set; }
    }
}
