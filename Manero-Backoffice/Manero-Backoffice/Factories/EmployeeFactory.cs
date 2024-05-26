using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Factories;

public class EmployeeFactory
{
    public static EmployeeRegistrationModel Create(string id, string firstname, string lastname)
    {
        return new EmployeeRegistrationModel
        {
            Id = id,
            FirstName = firstname,
            LastName = lastname,
        };
    }

    public static EmployeeRegistrationModel Create(string id, string firstname, string lastname, string employeeTypeId)
    {
        return new EmployeeRegistrationModel
        {
            Id = id,
            FirstName = firstname,
            LastName = lastname,
            EmployeeTypeId = employeeTypeId
        };
    }
}
