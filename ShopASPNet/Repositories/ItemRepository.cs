using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Data;
using ShopASPNet.Interfaces;
using ShopASPNet.Models.Items;

namespace ShopASPNet.Repositories; 

public class ItemRepository : IItemRepository {
    private readonly ShopDbContext _context;
    
    public ItemRepository(ShopDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync() {
        var items = await _context.Items.ToListAsync();
        return items;
    }

    public async Task<Item?> GetByIdAsync(int id) {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        return item;
    }

    public async Task<bool> AddAsync(Item item) {
        await _context.Items.AddAsync(item);
        return await SaveAsync();
    }

    public async Task<bool> RemoveByIdAsync(int id) {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (item is null)
            return false;

        _context.Items.Remove(item);
        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(Item item) {
        _context.Items.Update(item);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveAsync() {
        return await _context.SaveChangesAsync() > 0;
    }
}