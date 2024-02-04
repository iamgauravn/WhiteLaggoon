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
        public IActionResult Create(VillaNumberMM obj)
        { 
            var villNum = context.VillaNumbers.Where(n => n.Villa_Number == obj.VillaNumber.Villa_Number).FirstOrDefault();
               
            if (obj.VillaNumber.Villa_Number != null && villNum == null)
            {
                context.VillaNumbers.Add(obj.VillaNumber);
                context.SaveChangesAsync();
                return RedirectToAction("Index", "VillaNumber");
            } else
            {
                TempData["error"] = "The Villa number is already exist";
            }

            obj.VillaList = context.Villas.ToList().Select(n => new SelectListItem
            {
                Text = n.Name,
                Value = n.Id.ToString()
            });

            return View(obj);
        }

        [HttpGet("villaNumberId")]
        public IActionResult UpdateNumber(int villaNumberId)
        {

            VillaNumberMM villaNumberMM = new VillaNumberMM()
            {
                VillaList = context.Villas.ToList().Select(n => new SelectListItem
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                }),
                VillaNumber = context.VillaNumbers.FirstOrDefault(u=>u.VillaId == villaNumberId)
            };
         
             
            if(villaNumberMM.VillaNumber == null)
            {
                return RedirectToAction("Error","Home");
            }

            return View(villaNumberMM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberMM obj)
        {
                context.VillaNumbers.Update(obj.VillaNumber);
                context.SaveChangesAsync();
                return RedirectToAction("Index", "VillaNumber");
        }
         
        public IActionResult DeleteNumber(int villaNumberId)
        {
            VillaNumber? obj = context.VillaNumbers.FirstOrDefault(x => x.VillaId == villaNumberId);

            if(obj != null)
            {
                context.VillaNumbers.Remove(obj);
                context.SaveChanges();
                TempData["Success"] = "The Villa Has Been Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Villa Cannot be Deleted";
            return RedirectToAction("Error","Home");
        }

    }
}
