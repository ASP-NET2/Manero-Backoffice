using System.ComponentModel.DataAnnotations;

namespace Manero_Backoffice.Business.Models;

public class CategoryRegistrationForm
{
    [Required]
    public string CategoryName { get; set; } = null!;
    public string? ImageLink { get; set; }
}
