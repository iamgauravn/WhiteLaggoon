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


    }
}
