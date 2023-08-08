using Animal_Shelter.Data;
using Animal_Shelter.Data.Base;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Data.Services
{
    public class AnimalpicsServices : EntityBaseRepository<Animalpic>, IAnimalpicsService
    {
        public AnimalpicsServices(AppDbContext context) : base(context) { }
    }
}