using Animal_Shelter.Data;
using Animal_Shelter.Data.Base;
using Animal_Shelter.Models;

namespace Animal_Shelter.Data.Services
{
    public class NationalitysService : EntityBaseRepository<Nationality>, INationalitysService
    {
        public NationalitysService(AppDbContext context) : base(context) { }

    }
}