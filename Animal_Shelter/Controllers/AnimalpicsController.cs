using Animal_Shelter.Data;
using Animal_Shelter.Data.Services;
using Animal_Shelter.Data.Static;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AnimalpicsController : Controller
    {
        private readonly IAnimalpicsService _service;
        public AnimalpicsController(IAnimalpicsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get Animalpic /Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Animalpic animalpic)
        {
            if (!ModelState.IsValid)
            {
                return View(animalpic);
            }
            await _service.AddAsync(animalpic);
            return RedirectToAction(nameof(Index));
        }
        //Get : Animalpics/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(animalpicDetails);
        }

        //Get Animalpic/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var animalpicDetails = await _service.GetByIdAsync(id);
            if (animalpicDetails == null)
            {
                return View("NotFound");
            }
            return View(animalpicDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Animalpic animalpic)
        {
            if (!ModelState.IsValid)
            {
                return View(animalpic);
            }
            await _service.UpdateAsunc(id, animalpic);
            return RedirectToAction(nameof(Index));
        }

        //Get Animalpic/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var animalpicDetails = await _service.GetByIdAsync(id);
            if (animalpicDetails == null)
            {
                return View("NotFound");
            }
            return View(animalpicDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalpicDetails = await _service.GetByIdAsync(id);
            if (animalpicDetails == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}