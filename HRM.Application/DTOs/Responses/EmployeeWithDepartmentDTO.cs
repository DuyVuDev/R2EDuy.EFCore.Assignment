﻿namespace HRM.Application.DTOs.Responses
{
    public class EmployeeWithDepartmentDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public DateOnly JoinedDate { get; set; }
        public required string DepartmentName { get; set; }
    }
}
