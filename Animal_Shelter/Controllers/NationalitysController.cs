using Animal_Shelter.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Animal_Shelter.Data.Services;
using Microsoft.CodeAnalysis.VisualBasic;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Authorization;
using Animal_Shelter.Data.Static;

namespace Animal_Shelter.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class NationalitysController : Controller
    {
        private readonly INationalitysService _service;
        public NationalitysController(INationalitysService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allNationalitys);
        }
        //Get : producers/details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var nationalitysDetails = await _service.GetByIdAsync(id);
            if (nationalitysDetails == null) { return View("NotFound"); }
            return View(nationalitysDetails);

        }
        //Get : nationality /create/1
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Nationality nationality)
        {
            if (!ModelState.IsValid)
            {
                return View(nationality);
            }
            await _service.AddAsync(nationality);
            return RedirectToAction(nameof(Index));
        }

        //Get : nationality/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var nationalityDetails = await _service.GetByIdAsync(id);
            if (nationalityDetails == null) { return View("Notfound"); }
            return View(nationalityDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Nationality nationality)
        {
            if (!ModelState.IsValid)
            {
                return View(nationality);
            }
            if (id == nationality.Id)
            {
                await _service.UpdateAsunc(id, nationality);
                return RedirectToAction(nameof(Index));
            }
            return View(nationality);
        }
        //Get : nationality/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var nationalityDetails = await _service.GetByIdAsync(id);
            if (nationalityDetails == null) { return View("Notfound"); }
            return View(nationalityDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nationalityDetails = await _service.GetByIdAsync(id);
            if (nationalityDetails == null) { return View("Notfound"); }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}