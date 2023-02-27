using Microsoft.AspNetCore.Mvc;
using ShopASPNet.Data;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Interfaces; 

public interface IShoppingCartRepository {
    Task<ShoppingCart> GetCartByIdAsync(string id);
    Task<bool> AddAsync(ShoppingCart cart);
    Task<bool> SaveAsync();
    Task<bool> AddItemToCartAsync(string username, int itemId, int quantity);
}