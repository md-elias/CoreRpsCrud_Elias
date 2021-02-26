using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreRpsCrud_Elias.Data;
using CoreRpsCrud_Elias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CoreRpsCrud_Elias.Helper;

namespace CoreRpsCrud_Elias.Controllers
{
    public class AdmissionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdmissionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admissions.ToListAsync());
        }
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Admission());
            else
            {
                var AdmissionModel = await _context.Admissions.FindAsync(id);
                if (AdmissionModel == null)
                {
                    return NotFound();
                }
                return View(AdmissionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("AdmissionId,TraineeID,CourseName,InstractorName,TransanctionId,CourseFee,AdmissionDate")] Admission AdmissionModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    //AdmissionModel.AdmissionDate = DateTime.Now;
                    _context.Add(AdmissionModel);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    try
                    {
                        _context.Update(AdmissionModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdmissionModelExists(AdmissionModel.AdmissionId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Admissions.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", AdmissionModel) });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AdmissionModel = await _context.Admissions
                .FirstOrDefaultAsync(m => m.AdmissionId == id);
            if (AdmissionModel == null)
            {
                return NotFound();
            }

            return View(AdmissionModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var AdmissionModel = await _context.Admissions.FindAsync(id);
            _context.Admissions.Remove(AdmissionModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Admissions.ToList()) });
        }

        private bool AdmissionModelExists(int id)
        {
            return _context.Admissions.Any(e => e.AdmissionId == id);
        }
    }
}
