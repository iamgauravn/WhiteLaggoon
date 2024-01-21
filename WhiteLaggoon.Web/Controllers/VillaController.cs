using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLaggoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext context;
        public VillaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var villa = context.Villas;
            return View(villa);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            var villa = context.Villas.Where(x => x.Name == obj.Name).FirstOrDefault();

            if (villa != null)
            {
                ModelState.AddModelError(nameof(obj.Name), "Villa is Already Created");
                return View(villa);
            }

            obj.CreateDate = DateTime.Now;
            obj.UpdateDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                context.Villas.Add(obj);
                context.SaveChangesAsync();
                return RedirectToAction("Index", "Villa");
            }
            return View(obj);
        }

        [HttpGet("villaId")]
        public IActionResult Update(int villaId)
        {
            Villa? obj = context.Villas.FirstOrDefault(x => x.Id == villaId);

            if(obj == null)
            {
                return RedirectToAction("Error","Home");
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult UpdateVilla(Villa obj)
        {
            if(ModelState.IsValid && obj.Id > 0)
            {
                context.Villas.Update(obj);    
                context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
         
        public IActionResult Delete(int villaId)
        {
            Villa? obj = context.Villas.FirstOrDefault(x => x.Id == villaId);

            if(obj != null)
            {
                context.Villas.Remove(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error","Home");
        }

    }
}
