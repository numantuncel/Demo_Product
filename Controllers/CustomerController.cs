using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Demo_Product.Controllers
{
    public class CustomerController : Controller
    {
        JobMenager jobMenager = new JobMenager(new EfJobDal());
        CustomerMenager customerMenager = new CustomerMenager(new EfCustomerDal());
        public IActionResult Index()
        {
            var values = customerMenager.GetCustomersListWidthJob();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            
            List<SelectListItem> values = (from x in jobMenager.TGetList()
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.JobID.ToString(),
                                          }).ToList();  
            ViewBag.v=values;
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer p)
        {

            CustomerValidator validationRules = new CustomerValidator();
            ValidationResult results = validationRules.Validate(p);
            if (results.IsValid)
            {
                customerMenager.TInsert(p);
                return RedirectToAction("index");

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
        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            List<SelectListItem> values = (from x in jobMenager.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.JobID.ToString(),
                                           }).ToList();
            ViewBag.v = values;
            var value = customerMenager.GetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer p)
        {
            customerMenager.TUpdate(p);
            return RedirectToAction("index");
        }
        public IActionResult DeleteCustomer(int id)
        {
            var value = customerMenager.GetById(id);
            customerMenager.TDelete(value);
            return RedirectToAction("index");
        }
    }
}
