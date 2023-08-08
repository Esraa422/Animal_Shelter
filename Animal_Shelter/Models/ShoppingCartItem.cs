using System.ComponentModel.DataAnnotations;

namespace Animal_Shelte.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Animal Animal { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}