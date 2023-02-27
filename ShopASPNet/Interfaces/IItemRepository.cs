using ShopASPNet.Data;
using ShopASPNet.Models.Items;

namespace ShopASPNet.Interfaces; 

public interface IItemRepository {
    Task<IEnumerable<Item>> GetAllAsync();
    Task<Item?> GetByIdAsync(int id);
    Task<bool> AddAsync(Item item);
    Task<bool> RemoveByIdAsync(int id);
    Task<bool> UpdateAsync(Item item);
    Task<bool> SaveAsync();
}