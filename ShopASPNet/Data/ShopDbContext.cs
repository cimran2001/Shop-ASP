using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Models.Items;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Data; 

#nullable disable

public class ShopDbContext : IdentityDbContext<AppUser> {
    public DbSet<Item> Items { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        
        builder.Entity<Item>(item => {
            item.HasMany(i => i.ShoppingCartItems)
                .WithOne(sci => sci.Item)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}