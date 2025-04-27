using AdminDashboard.Helpers;
using AdminDashboard.Models.Products;
using AutoMapper;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
             var mappedProducts = _mapper.Map<IEnumerable<ProductViewModel>>(Products);
            
            return View(mappedProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async  Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                    productViewModel.PictureUrl = PictureSettings.Upload(productViewModel.Image, "products");

                else
                    productViewModel.PictureUrl = "images/products/blueberry - cheesecake.png";

                var mappedProduct = _mapper.Map<Product>(productViewModel);
              
                mappedProduct.NormalizedName = productViewModel.Name.ToUpper();
                mappedProduct.CreatedBy = User.Identity?.Name;
                mappedProduct.LastModifiedBy = User.Identity?.Name;


                await _unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();


                return (RedirectToAction("Index"));

            }
            else
                return View(productViewModel);
        }
    }
}
