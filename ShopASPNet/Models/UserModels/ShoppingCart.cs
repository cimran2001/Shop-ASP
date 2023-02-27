using ShopASPNet.Models.Items;

namespace ShopASPNet.Models.UserModels; 

#nullable disable

public class ShoppingCart {
    public int Id { get; set; }
    public AppUser User { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }
}