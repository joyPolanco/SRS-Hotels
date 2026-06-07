namespace SRS_Hotels.Data.src.Modules.Employees.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid WorkShiftId { get; set; }

        public EmployeeStatus Status { get; set; }
    }
}
