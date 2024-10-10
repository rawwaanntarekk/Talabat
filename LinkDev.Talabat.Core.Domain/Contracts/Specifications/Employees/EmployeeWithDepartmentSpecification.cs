using LinkDev.Talabat.Core.Domain.Employees;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications.Employees
{
	public class EmployeeWithDepartmentSpecification : BaseSpecifications<Employee, int>
	{
		public EmployeeWithDepartmentSpecification() : base()
		{
			Includes.Add(x => x.Department!);
		}

        public EmployeeWithDepartmentSpecification(int id) : base(id)
        {
			Includes.Add(x => x.Department!);

		}

	}
	
}
