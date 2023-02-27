using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Data;
using ShopASPNet.Interfaces;
using ShopASPNet.Models.UserModels;
using ShopASPNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string databaseName = "ShopDbSqlite";
var connectionString = builder.Configuration.GetConnectionString(databaseName) ??
                       throw new InvalidOperationException($"Connection string '{databaseName}' not found.");
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => {
    // options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<ShopDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/User/AccessDenied";
    options.Cookie.Name = "ShopCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/User/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

// Dependency Injection
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();