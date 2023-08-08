using Animal_Shelter.Data.ViewModels;
using Animal_Shelter.Data.Base;
using Animal_Shelter.Data.ViewModels;
using Animal_Shelter.Models;
using Microsoft.Build.Framework;

namespace Animal_Shelter.Data.Services
{
    public interface IAnimalsService : IEntityBaseRepository<Animal>
    {
        Task<Animal> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewAnimalAsync(NewAnimalVM data);
        Task UpdateAnimalAsync(NewAnimalVM data);
        Task DeleteAnimalAsync(int id);


    }
}