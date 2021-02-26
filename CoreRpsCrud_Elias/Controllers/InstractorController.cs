using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreRpsCrud_Elias.Data;
using CoreRpsCrud_Elias.Models;
using CoreRpsCrud_Elias.ViewModels;


namespace CoreRpsCrud_Elias.Controllers
{
    public class InstractorController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment webHostEnvironment;
        public InstractorController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            db = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Instractor.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instractor = await db.Instractor
                .FirstOrDefaultAsync(m => m.InstractorID == id);

            var instractorViewModel = new InstractorViewModel()
            {
                InstractorID = instractor.InstractorID,
                InstractorName = instractor.InstractorName,
                EmailAddress = instractor.EmailAddress,
                CellPhone = instractor.CellPhone,
                ContactAddress = instractor.ContactAddress,
                JoiningDate = instractor.JoiningDate,             
                ExistingImage = instractor.ProfilePicture
            };

            if (instractor == null)
            {
                return NotFound();
            }

            return View(instractor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstractorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Instractor instractor = new Instractor
                {
                    InstractorName = model.InstractorName,
                    EmailAddress = model.EmailAddress,
                    CellPhone = model.CellPhone,
                    ContactAddress = model.ContactAddress,
                    JoiningDate = model.JoiningDate,                
                    ProfilePicture = uniqueFileName
                };

                db.Add(instractor);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instractor = await db.Instractor.FindAsync(id);
            var instractorViewModel = new InstractorViewModel()
            {
                InstractorID = instractor.InstractorID,
                InstractorName = instractor.InstractorName,
                EmailAddress = instractor.EmailAddress,
                CellPhone = instractor.CellPhone,
                ContactAddress = instractor.ContactAddress,
                JoiningDate = instractor.JoiningDate,
                ExistingImage = instractor.ProfilePicture
            };

            if (instractor == null)
            {
                return NotFound();
            }
            return View(instractorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstractorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var instractor = await db.Instractor.FindAsync(model.Id);
                instractor.InstractorName = model.InstractorName;
                instractor.EmailAddress = model.EmailAddress;
                instractor.CellPhone = model.CellPhone;
                instractor.ContactAddress = model.ContactAddress;
                instractor.JoiningDate = model.JoiningDate;              

                if (model.ProfilePicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    instractor.ProfilePicture = ProcessUploadedFile(model);
                }
                db.Update(instractor);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instractor = await db.Instractor
                .FirstOrDefaultAsync(m => m.InstractorID == id);

            var instractorViewModel = new InstractorViewModel()
            {
                InstractorID = instractor.InstractorID,
                InstractorName = instractor.InstractorName,
                EmailAddress = instractor.EmailAddress,
                CellPhone = instractor.CellPhone,
                ContactAddress = instractor.ContactAddress,
                JoiningDate = instractor.JoiningDate,
                ExistingImage = instractor.ProfilePicture
            };
            if (instractor == null)
            {
                return NotFound();
            }

            return View(instractorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instractor = await db.Instractor.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", instractor.ProfilePicture);
            db.Instractor.Remove(instractor);
            if (await db.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InstractorExists(int id)
        {
            return db.Instractor.Any(e => e.InstractorID == id);
        }

        private string ProcessUploadedFile(InstractorViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
