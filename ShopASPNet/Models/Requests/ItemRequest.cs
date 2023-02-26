namespace ShopASPNet.Models.Requests; 

#nullable disable

public class ItemRequest {
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}