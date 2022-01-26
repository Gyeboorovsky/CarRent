using CarRent.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRent.DAL;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? DrivingLicenceNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public ICollection<Rent> Rents { get; set; }
}