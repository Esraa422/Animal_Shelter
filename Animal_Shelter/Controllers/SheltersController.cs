using Animal_Shelter.Data;
using Animal_Shelter.Data.Services;
using Animal_Shelter.Data.Static;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SheltersController : Controller
    {
        private readonly ISheltersService _service;
        public SheltersController(ISheltersService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allShelters = await _service.GetAllAsync();
            return View(allShelters);
        }
        //Get : Shelter /create/1
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Shelter shelter)
        {
            if (!ModelState.IsValid)
            {
                return View(shelter);
            }
            await _service.AddAsync(shelter);
            return RedirectToAction(nameof(Index));
        }
        //Get : Shelters/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var shelterDetails = await _service.GetByIdAsync(id);
            if (shelterDetails == null)
            {
                return View("NotFound");
            }
            return View(shelterDetails);
        }

        //Get:shelter /Edit1
        public async Task<IActionResult> Edit(int id)
        {
            var shelterDetails = await _service.GetByIdAsync(id);
            if (shelterDetails == null)
            {
                return View("NotFound");
            }
            return View(shelterDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Shelter shelter)
        {
            if (!ModelState.IsValid)
            {
                return View(shelter);
            }
            await _service.UpdateAsunc(id, shelter);
            return RedirectToAction(nameof(Index));
        }
        //Get:shelter /Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var shelterDetails = await _service.GetByIdAsync(id);
            if (shelterDetails == null)
            {
                return View("NotFound");
            }
            return View(shelterDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var shelterDetails = await _service.GetByIdAsync(id);
            if (shelterDetails == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}