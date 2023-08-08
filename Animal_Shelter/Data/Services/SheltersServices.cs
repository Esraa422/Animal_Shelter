using Animal_Shelter.Data;
using Animal_Shelter.Data.Base;
using Animal_Shelter.Models;

namespace Animal_Shelter.Data.Services
{
    public class SheltersService : EntityBaseRepository<Shelter>, ISheltersService
    {
        public SheltersService(AppDbContext context) : base(context) { }

    }
}