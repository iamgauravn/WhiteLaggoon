using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLaggoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository villaRepo;
        public VillaController(IVillaRepository villaRepo)
        {
            this.villaRepo = villaRepo;
        }

        public IActionResult Index()
        {
            var villa = villaRepo.GetAll();
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
                villaRepo.Add(obj);
                villaRepo.Save();
                return RedirectToAction("Index", "Villa");
            }
            return View(obj);
        }

        [HttpGet("villaId")]
        public IActionResult Update(int villaId)
        {
            Villa? obj = villaRepo.GetById(villaId);

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
                villaRepo.Update(obj);
                villaRepo.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
         
        public IActionResult Delete(int villaId)
        {
            Villa? obj = villaRepo.GetById(villaId);

            if(obj != null)
            {
                villaRepo.Remove(obj);
                villaRepo.Save();
                TempData["Success"] = "The Villa Has Been Deleted Successfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Villa Cannot be Deleted";
            return RedirectToAction("Error","Home");
        }

    }
}
