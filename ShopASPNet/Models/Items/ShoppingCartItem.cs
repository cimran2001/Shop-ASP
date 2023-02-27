namespace ShopASPNet.Models.Items; 

#nullable disable

public class ShoppingCartItem {
    public int Id { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
}