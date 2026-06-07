namespace SRS_Hotels.Data.src.Modules.Employees.Domain
{
    public class WorkShift
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
