﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IEmployeeService> _employeeService;

		public IProductService ProductService => _productService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;


		public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
			_employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(_unitOfWork, _mapper));
		}
    }
}
