using Animal_Shelter.Data.Base;
using Animal_Shelter.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Animal_Shelter.Models
{
    public class Animal : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime ComingDate { get; set; }
        public DateTime ExpectedBirthday { get; set; }
        public AnimalType  AnimalType { get; set; }
        //Relationships
        public List<Animalpic_Animal>? Animalpic_Animal { get; set; }
        //Shelter
        public int ShelterId { get; set; }
        [ForeignKey("ShelterId")]
        public Shelter Shelter { get; set; }
        //Nationality
        public int NationalityId { get; set; }
        [ForeignKey("NationalityId")]
        public Nationality Nationality { get; set; }
    }
}