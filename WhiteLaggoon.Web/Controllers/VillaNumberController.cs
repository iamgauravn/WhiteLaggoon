using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLaggoon.Web.ViewModels;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLaggoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext context;
        public VillaNumberController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var villaNumbers = context.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(villaNumbers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            VillaNumberMM villaNumberMM = new VillaNumberMM()
            {
                VillaList = context.Villas.ToList().Select(n => new SelectListItem
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
            }; 

            return View(villaNumberMM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        { 
            var villNum = context.VillaNumbers.Where(n => n.VillaId == obj.VillaId).FirstOrDefault();

            if(villNum != null)
            {
                ModelState.AddModelError(nameof(obj.VillaId), "Villa Already Exist");
                return View(obj);
            }

            if (ModelState.IsValid)
            {
                context.VillaNumbers.Add(obj);
                context.SaveChangesAsync();
                return RedirectToAction("Index", "VillaNumber");
            } else
            {
                ModelState.AddModelError(nameof(obj), "");
            }
            return View(obj);
        }

        [HttpGet("villaNumberId")]
        public IActionResult Update(int villaNumberId)
        {
            Villa? obj = context.Villas.FirstOrDefault(x => x.Id == villaNumberId);

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
         
        public IActionResult Delete(int villaNumberId)
        {
            Villa? obj = context.Villas.FirstOrDefault(x => x.Id == villaNumberId);

            if(obj != null)
            {
                context.Villas.Remove(obj);
                context.SaveChanges();
                TempData["Success"] = "The Villa Has Been Deleted Successfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Villa Cannot be Deleted";
            return RedirectToAction("Error","Home");
        }

    }
}
