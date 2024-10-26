using AutoMapper;
using LinkDev.Talabat.Core.Application.Common.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.Dashboard.Helpers;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork,
                                   IMapper _mapper
                                   ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IEnumerable<Product> , IEnumerable<ProductViewModel>>(products);

            return View(mappedProducts);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image != null)
                {
                    model.PictureURL = PictureSettings.UploadFile(model.Image, "products");
                }
                else
                {
                    model.PictureURL = "images/products/iced-caramel-macchiato.png";
                }

                var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);

                #region Static data - Need to be modified

                mappedProduct.CreatedBy = "Rawan";
                mappedProduct.LastModifiedBy = "Rawan";
                mappedProduct.NormalizedName = model.Name.ToUpper();  
                #endregion


                await _unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);

                await _unitOfWork.CompleteAsync();


                return RedirectToAction("Index");




            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product!);

            return View(mappedProduct);

            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(model.Image != null)
                {
                    PictureSettings.DeleteFile(model.PictureURL, "products");
                    model.PictureURL = PictureSettings.UploadFile(model.Image, "products");
                }


               



                var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);
                mappedProduct.PictureUrl = mappedProduct.PictureUrl ?? "model.PictureURL = \"images/products/iced-caramel-macchiato.png\";";

                #region Static Data
                mappedProduct.CreatedBy = "Rawan";
                mappedProduct.LastModifiedBy = "Rawan";
                mappedProduct.NormalizedName = model.Name.ToUpper();
                mappedProduct.LastModifiedBy = "Rawan"; 
                #endregion

                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);

                var result = await _unitOfWork.CompleteAsync();
                if(result > 0) 
                    return RedirectToAction("Index");
            }
                return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product!);

            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete (int id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);

                if (product.PictureUrl != null)
                {
                    PictureSettings.DeleteFile(product.PictureUrl, "products");
                }
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            } catch(Exception)
            {
                return View(model);
            }

        }
    }
}
