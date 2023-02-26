using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Data;
using ShopASPNet.Models.Requests;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Controllers; 

public class ProfileController : Controller {
    private readonly ShopDbContext _context;
    
    public ProfileController(ShopDbContext context) {
        _context = context;
    }

    public async Task<IActionResult> Index() {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => User.Identity != null && u.UserName == User.Identity.Name);
        if (user is null)
            return RedirectToAction("Error", "Home");
        
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit() {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => User.Identity != null && u.UserName == User.Identity.Name);
        if (user is null)
            return RedirectToAction("Error", "Home");

        var request = new UserSignUpRequest {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserSignUpRequest request) {
        var user = await _context.Users.FirstOrDefaultAsync(u => User.Identity != null && u.UserName == User.Identity.Name);
        if (user is null) {
            TempData["Error"] = "Something went wrong";
            return await Edit();
        }

        if (!_context.Users.Any(u => u.UserName == request.Username))
            user.UserName = request.Username;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}