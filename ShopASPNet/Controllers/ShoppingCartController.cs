using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Data;
using ShopASPNet.Interfaces;
using ShopASPNet.Models.Items;
using ShopASPNet.Models.Requests;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Controllers; 

[Controller]
public class ShoppingCartController : Controller {
    private readonly UserManager<AppUser> _userManager;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ShopDbContext _context;
    
    public ShoppingCartController(UserManager<AppUser> userManager, IShoppingCartRepository shoppingCartRepository, ShopDbContext context) {
        _userManager = userManager;
        _shoppingCartRepository = shoppingCartRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index() {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user is null)
            return Forbid();

        var userId = user.Id;
        var cart = await _shoppingCartRepository.GetCartByIdAsync(userId);

        return View(cart);
    }

    [HttpGet]
    public async Task<IActionResult> Add(int itemId) {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
        if (item is null)
            return RedirectToAction("Index");
        
        var cartItem = new ItemToCartRequest {
            ItemId = itemId,
            Quantity = 1,
        };
        return View(cartItem);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ItemToCartRequest request) {
        if (User.Identity?.Name != null)
            await _shoppingCartRepository.AddItemToCartAsync(User.Identity.Name, request.ItemId, request.Quantity);
        return RedirectToAction("Index");
    }
}