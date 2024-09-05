using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Product.Controllers
{
    public class JobController : Controller
    {
        JobMenager JobMenager = new JobMenager(new EfJobDal());
        public IActionResult Index()
        {
            var values = JobMenager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddJob(Job p)
        {
            JobMenager.TInsert(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateJob(int id)
        {
            var values = JobMenager.GetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateJob(Job p)
        {
            JobMenager.TUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteJob(int id)
        {
            var values=JobMenager.GetById(id);
            JobMenager.TDelete(values);
            return RedirectToAction("Index");

        }
    }
}
