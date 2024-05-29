using System.ComponentModel.DataAnnotations;

namespace Manero_Backoffice.Business.Models;

public class PromoCodeRegistrationForm
{
    [Required]
    public string CodeName { get; set; } = null!;
    [Required]
    public int Discount { get; set; }
    public DateTime ExpiresDate { get; set; } = DateTime.Now.AddDays(7);
    public bool IsActive { get; set; } = true;
    public bool IsPrivate { get; set; }
    public string? UserId { get; set; }
}
