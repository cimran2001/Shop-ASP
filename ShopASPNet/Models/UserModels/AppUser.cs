using Microsoft.AspNetCore.Identity;

namespace ShopASPNet.Models.UserModels; 

#nullable disable

public class AppUser : IdentityUser {
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
