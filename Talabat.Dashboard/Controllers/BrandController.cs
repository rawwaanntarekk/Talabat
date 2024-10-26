using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
    public class BrandController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return View(brands);
        }

        public async Task<IActionResult> Create(ProductBrandViewModel model)
        {
            if (!ModelState.IsValid) {
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());

            }

            var brand = new ProductBrand
            {
                Id = 0,
                Name = model.Name,
                CreatedBy = "Rawan",
                LastModifiedBy = "Rawan"
            };

            await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(brand);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);

            _unitOfWork.GetRepository<ProductBrand, int>().Delete(brand);

            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
