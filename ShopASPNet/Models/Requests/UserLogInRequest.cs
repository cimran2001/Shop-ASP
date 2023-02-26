namespace ShopASPNet.Models.Requests; 

#nullable disable

public class UserLogInRequest {
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}