namespace Manero_Backoffice.Business.Models;

public class EmployeeUpdateModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? EmployeeTypeId { get; set; }
    public string? AddressId { get; set; }

}
