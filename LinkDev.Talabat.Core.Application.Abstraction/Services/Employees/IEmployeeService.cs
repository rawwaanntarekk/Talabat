using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeToReturnDTO>> GetAllEmployeeAsync();

		Task<EmployeeToReturnDTO> GetEmployeeAsync(int id);

	}
}
