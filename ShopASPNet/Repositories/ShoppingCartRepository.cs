using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Data;
using ShopASPNet.Interfaces;
using ShopASPNet.Models.Items;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Repositories; 

public class ShoppingCartRepository : IShoppingCartRepository {
    private readonly ShopDbContext _context;

    public ShoppingCartRepository(ShopDbContext context, UserManager<AppUser> userManager) {
        _context = context;
    }
    
    public async Task<ShoppingCart> GetCartByIdAsync(string id) {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
            return null;

        var cart = await _context.ShoppingCarts
            .Include(sc => sc.User)
            .Include(sc => sc.ShoppingCartItems)
            .ThenInclude(sci => sci.Item)
            .FirstOrDefaultAsync(sc => sc.User.Id == user.Id);

        if (cart is null) {
            cart = new ShoppingCart {
                User = user,
                ShoppingCartItems = new List<ShoppingCartItem>(),
            };
            await AddAsync(cart);
        }
        
        return cart;
    }

    public async Task<bool> AddAsync(ShoppingCart cart) {
        await _context.ShoppingCarts.AddAsync(cart);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync() {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddItemToCartAsync(string username, int itemId, int quantity) {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
        if (item is null)
            return false;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user is null)
            return false;

        var cart = await GetCartByIdAsync(user.Id);

        var sci = cart.ShoppingCartItems.FirstOrDefault(sci => sci.Item.Id == item.Id);
        if (sci is null) {
            var cartItem = new ShoppingCartItem {
                Item = item,
                Quantity = quantity
            };
            cart.ShoppingCartItems.Add(cartItem);
        }
        else {
            sci.Quantity += quantity;
        }
        
        return await SaveAsync();
    }
}