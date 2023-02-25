using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShopASPNet.Data; 

public class ShopDbContext : IdentityDbContext {
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
}