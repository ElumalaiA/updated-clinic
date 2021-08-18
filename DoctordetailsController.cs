using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Data;
using ClinicManagement.Domin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ClinicManagementMVC.Controllers
{
    public class DoctordetailsController : Controller
    {
        private readonly ClinicContext _context;

        public DoctordetailsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Doctordetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctordetails.ToListAsync());
        }
        public async Task<IActionResult> GetAllDoctors()
        {

            List<Doctordetails> docs = new List<Doctordetails>();
            docs = _context.Doctordetails.Select(d => d).ToList();

            return Json(new { data = docs });
        }

        // GET: Doctordetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var doctordetails = await _context.Doctordetails
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctordetails == null)
            {
                return NotFound();
            }

            return View(doctordetails);
        }

        // GET: Doctordetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctordetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorID,FirstName,LastName,Sex,Age,Date_of_Birth,Specialization,FromTime,ToTime,PhoneNumber")] Doctordetails doctordetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctordetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctordetails);
        }
        [HttpGet]
        public PartialViewResult Creating()
        {
            return PartialView("Creating", new ClinicManagement.Domin.Doctordetails());
        }
        [HttpPost]
        public JsonResult Creating(Doctordetails ddetail)
        {
            _context.Doctordetails.Add(ddetail);
            _context.SaveChanges();
            return Json(ddetail, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        // GET: Doctordetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctordetails = await _context.Doctordetails.FindAsync(id);
            if (doctordetails == null)
            {
                return NotFound();
            }
            return View(doctordetails);
        }

        // POST: Doctordetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorID,FirstName,LastName,Sex,Age,Date_of_Birth,Specialization,FromTime,ToTime,PhoneNumber")] Doctordetails doctordetails)
        {
            if (id != doctordetails.DoctorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctordetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctordetailsExists(doctordetails.DoctorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctordetails);
        }
        [HttpGet]
        public ActionResult Editing(int DoctorID)
        {
            var d = _context.Doctordetails.Find(DoctorID);
            if (d == null)
            {
                return NotFound();
            }

            return PartialView("Editing", d);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editing(Doctordetails doc)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(doc).State = EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Editing", doc);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctordetails = await _context.Doctordetails
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctordetails == null)
            {
                return NotFound();
            }

            return View(doctordetails);
        }

        // POST: Doctordetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctordetails = await _context.Doctordetails.FindAsync(id);
            _context.Doctordetails.Remove(doctordetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //public ActionResult deleted(int id = 0)
        //{
        //    Doctordetails doc = _context.Doctordetails.Find(id);
        //    if (doc == null)
        //    {
        //        return NotFound();
        //    }
        //    return PartialView("deleted", doc);
        //}


        ////
        //// POST: /Phone/Delete/5
        //[HttpPost, ActionName("deleted")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletedConfirmed(int id)
        //{
        //    var obj = _context.Doctordetails.Find(id);
        //    _context.Doctordetails.Remove(obj);
        //    _context.SaveChanges();
        //    return Json(new { success = true });
        //}

        private bool DoctordetailsExists(int id)
        {
            return _context.Doctordetails.Any(e => e.DoctorID == id);
        }
    }
}
