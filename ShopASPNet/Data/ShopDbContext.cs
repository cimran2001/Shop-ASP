using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Data; 

public class ShopDbContext : IdentityDbContext<AppUser> {
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
}