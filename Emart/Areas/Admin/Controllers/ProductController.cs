using Emart.DataAccessLayer.Infrastructure.IRepository;
using Emart.Models;
using Emart.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Emart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.Products = _unitOfWork.Product.GetAll();
            return View(productVM);
        }
 
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = _unitOfWork.Category.GetAll().Select(x=> 
                new SelectListItem()
                {
                    Text = x.Name,
                    Value =x.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(vm.Product.Id==0)
                {
                    _unitOfWork.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done!";
                }
                else
                {
                    _unitOfWork.Product.Update(vm.Product);
                    TempData["success"] = "Product Updated Done!";
                }

                
                _unitOfWork.Save();
               
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _unitOfWork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var product = _unitOfWork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Delete(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Done!";
            return RedirectToAction("Index");
        }


    }
}
