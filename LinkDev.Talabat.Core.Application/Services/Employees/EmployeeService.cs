using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications.Employees;
using LinkDev.Talabat.Core.Domain.Employees;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
	internal class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
	{
		public async Task<IEnumerable<EmployeeToReturnDTO>> GetAllEmployeeAsync()
		{
			var specification = new EmployeeWithDepartmentSpecification();

			var employees = await unitOfWork.GetRepository<Employee, int>().GetAllAsyncWithSpec( specification);

			var employeesToReturn = mapper.Map<IEnumerable<EmployeeToReturnDTO>>(employees);

			return employeesToReturn;
		}

		public async Task<EmployeeToReturnDTO> GetEmployeeAsync(int id)
		{
			var specification = new EmployeeWithDepartmentSpecification(id);

			var employee = await unitOfWork.GetRepository<Employee, int>().GetAsyncWithSpec(specification);

			var employeeToReturn = mapper.Map<EmployeeToReturnDTO>(employee);

			return employeeToReturn;
		}
	}
}
