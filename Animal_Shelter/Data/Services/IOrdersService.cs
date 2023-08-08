using Animal_Shelte.Models;
using Animal_Shelter.Models;
using Animal_Shelter.Models;

namespace Animal_Shelter.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}