using Animal_Shelter.Data.ViewModels;
using Animal_Shelter.Data;
using Animal_Shelter.Data.Base;
using Animal_Shelter.Data.ViewModels;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Data.Services
{
    public class AnimalsService : EntityBaseRepository<Animal>, IAnimalsService
    {
        private readonly AppDbContext _context;
        public AnimalsService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddNewAnimalAsync(NewAnimalVM data)
        {
            var newAnimal = new Animal()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                ShelterId = data.ShelterId,
                ComingDate = data.ComingDate,
                ExpectedBirthday = data.ExpectedBirthday,
                AnimalType = data.AnimalType,
                NationalityId = data.NationalityId
            };
            await _context.Animal.AddAsync(newAnimal);
            await _context.SaveChangesAsync();

            //Add animal Actors
            foreach (var animalpicId in data.animalpicIds)
            {
                var AnimalpicAnimal = new Animalpic_Animal()
                {
                    AnimalId = newAnimal.Id,
                    animalpicId = animalpicId
                };
                await _context.Animalpic_Animal.AddAsync(newAnimalpicAnimal);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<Animal> GetMovieByIdAsync(int id)
        {
            var animalDetails = await _context.Animals
                .Include(c => c.Shelter)
                .Include(p => p.Nationality)
                .Include(am => am.Animalpic_Animal).ThenInclude(a => a.Animalpic)
                .FirstOrDefaultAsync(n => n.Id == id);
            return animalDetails;
        }
        public async Task<newAnimalDropdownsVM> GetnewAnimalDropdownsValues()
        {
            var response = new newAnimalDropdownsVM()
            {
                Animalpics = await _context.Animalpics.OrderBy(n => n.FullName).ToListAsync(),
                Shelters = await _context.Shelters.OrderBy(n => n.Name).ToListAsync(),
                Nationalitys = await _context.Nationalitys.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
        public async Task UpdateAnimalAsync(NewAnimalVM data)
        {
            var dbAnimal = await _context.Animals.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbAnimal != null)
            {
                dbAnimal.Name = data.Name;
                dbAnimal.Description = data.Description;
                dbAnimal.Price = data.Price;
                dbAnimal.ImageURL = data.ImageURL;
                dbAnimal.ShelterId = data.ShelterId;
                dbAnimal.ComingDate = data.ComingDate;
                dbAnimal.ExpectedBirthday = data.ExpectedBirthday;
                dbAnimal.AnimalType = data.AnimalType;
                dbAnimal.NationalityId = data.NationalityId;
                await _context.SaveChangesAsync();
            }

            //Remove existing animalpic
            var existingActorsDb = _context.Animalpic_Animal.Where(n => n.AnimalId == data.Id).ToList();
            _context.Animalpic_Animal.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Animal Animalpic
            foreach (var animalpicId in data.AnimalpicIds)
            {
                var newAnimalpicAnimal = new Animalpic_Animal()
                {
                    AnimalId = data.Id,
                    AnimalpicId = animalpicId
                };
                await _context.Animalpic_Animal.AddAsync(newAnimalpicAnimal);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAnimalAsync(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }
            await _context.SaveChangesAsync();

        }


    }
}