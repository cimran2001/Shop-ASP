using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Models.Items;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Data; 

#nullable disable

public class ShopDbContext : IdentityDbContext<AppUser> {
    public DbSet<Item> Items { get; set; }
    
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
}