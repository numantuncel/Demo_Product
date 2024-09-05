using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;


namespace Demo_Product.Controllers
{
    public class ProductController1 : Controller
    {
        ProductMenager productMenager = new ProductMenager(new EfProductDal());
        public IActionResult Index()
        {
            var values = productMenager.TGetList();
            return View(values);
        }
        [HttpGet]// çağrıldığında 
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            ProductValidator validationRules = new ProductValidator();
            ValidationResult results = validationRules.Validate(p);
            if (results.IsValid)
            {
                productMenager.TInsert(p);
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public IActionResult DeleteProduct(int id)
        {
            var value = productMenager.GetById(id);
            productMenager.TDelete(value);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var value=productMenager.GetById(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product p)
        {
                productMenager.TUpdate(p);
                return RedirectToAction("Index");

        }
    }
}


//return View(); ifadesi, varsayılan olarak metod adıyla aynı olan bir görünümü (view) döndürür.
//Bu durumda, "AddProduct" adında bir görünüm (genellikle /Views/ControllerName/AddProduct.cshtml yolunda
//bulunur) geri döndürülür.
//Bu metod çağrıldığında, tarayıcıya "AddProduct" görünümü render edilip gönderilir.