using System.Runtime.InteropServices.JavaScript;

namespace ShopASPNet.Models.Items; 

#nullable disable

public class Item {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastEdited { get; set; }
    // public string ImagePath { get; set; }
}