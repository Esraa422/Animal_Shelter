using Animal_Shelter.Data;
using Animal_Shelter.Data.Services;
using Animal_Shelter.Data.Static;
using Animal_Shelter.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _service;
        public AnimalsController(IAnimalsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allAnimals = await _service.GetAllAsync(n => n.Shelter);
            return View(allAnimals);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allAnimals = await _service.GetAllAsync(n => n.Shelter);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allAnimals.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }
            return View("Index", allAnimals);
        }
        //Get: Animal /Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var animalDetails = await _service.GetaAnimalByIdAsync(id);
            return View(animalDetails);
        }
        //Get: Animals /Creat
        public async Task<IActionResult> Create()
        {
            var animalDropdownsData = await _service.GetNewAnimalDropdownsValues();
            ViewBag.Shelters = new SelectList(animalDropdownsData.Shelters, "Id", "Name");
            ViewBag.Nationalitys = new SelectList(animalDropdownsData.Nationalitys, "Id", "FullName");
            ViewBag.Animalpic = new SelectList(animalDropdownsData.Animalpic, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewAnimalVM animal)
        {
            if (!ModelState.IsValid)
            {
                var animalDropdownsData = await _service.GetNewAnimalDropdownsValues();

                ViewBag.Shelters = new SelectList(animalDropdownsData.Shelters, "Id", "Name");
                ViewBag.Nationalitys = new SelectList(animalDropdownsData.Nationalitys, "Id", "FullName");
                ViewBag.Animalpic = new SelectList(animalDropdownsData.Animalpic, "Id", "FullName");

                return View(animal);
            }
            await _service.AddNewanimalAsync(animal);
            return RedirectToAction(nameof(Index));

        }
        //GET: animals/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var animalDetails = await _service.GetaAnimalByIdAsync(id);
            if (animalDetails == null) return View("NotFound");

            var response = new NewAnimalVM()
            {
                Id = animalDetails.Id,
                Name = animalDetails.Name,
                Description = animalDetails.Description,
                Price = animalDetails.Price,
                ComingDate = animalDetails.ComingDate,
                ExpectedBirthday = animalDetails.ExpectedBirthday,
                ImageURL = animalDetails.ImageURL,
                AnimalType = animalDetails.AnimalType,
                ShelterId = animalDetails.ShelterId,
                NationalityId = animalDetails.NationalityId,
                AnimalpicIds = animalDetails.Animalpic_Animal.Select(n => n.AnimalpicId).ToList(),
            };

            var animalDropdownsData = await _service.GetNewAnimalDropdownsValues();
            ViewBag.Shelters = new SelectList(animalDropdownsData.Shelters, "Id", "Name");
            ViewBag.Nationalitys = new SelectList(animalDropdownsData.Nationalitys, "Id", "FullName");
            ViewBag.Animalpic = new SelectList(animalDropdownsData.Animalpic, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewAnimalVM animal)
        {
            if (id != animal.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var animalDropdownsData = await _service.GetNewAnimalDropdownsValues();

                ViewBag.Shelters = new SelectList(animalDropdownsData.Shelters, "Id", "Name");
                ViewBag.Nationalitys = new SelectList(animalDropdownsData.Nationalitys, "Id", "FullName");
                ViewBag.Animalpic = new SelectList(animalDropdownsData.Animalpic, "Id", "FullName");

                return View(animal);
            }
            await _service.UpdateanimalAsync(animal);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAnimalAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}