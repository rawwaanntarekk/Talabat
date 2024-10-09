namespace LinkDev.Talabat.Core.Domain.Employees
{
	public class Department : BaseAuditEntity<int>
	{
		public string Name { get; set; } = string.Empty;
        public DateOnly CreationDate { get; set; }

    }
}
