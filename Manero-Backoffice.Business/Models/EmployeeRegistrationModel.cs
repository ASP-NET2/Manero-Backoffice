namespace Manero_Backoffice.Business.Models;

public class EmployeeRegistrationModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? EmployeeTypeId { get; set; }
    public string? AddressId { get; set; }
}
