namespace LinkDev.Talabat.Core.Domain.Employees
{
	public class Employee : BaseAuditEntity<int>
	{
        public required string Name { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
