namespace ShopASPNet.Models.Requests; 

#nullable disable

public class ItemToCartRequest {
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}