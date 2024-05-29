namespace Manero_Backoffice.Business.Models;

public class PromocodeModel
{
    public string Id { get; set; } = null!;
    public string CodeName { get; set; } = null!;
    public int Discount { get; set; }
    public DateTime ExpiresDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsPrivate { get; set; }
    public string? UserId { get; set; }
}
