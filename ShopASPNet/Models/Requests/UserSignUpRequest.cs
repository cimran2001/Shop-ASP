using System.ComponentModel.DataAnnotations;

namespace ShopASPNet.Models.Requests; 

#nullable disable

public class UserSignUpRequest {
    [Required]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "The length should be between 8 and 30 inclusive")]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [Compare(nameof(Password))]
    public string RepeatPassword { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "The length should be between 8 and 30 inclusive")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "The length should be between 8 and 30 inclusive")]
    public string LastName { get; set; }
}