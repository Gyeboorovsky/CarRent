using CarRent.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.DAL;

public class Vehicle
{
    [Key]
    public Guid Id { get; set; }
    //TODO to enum
    public string Brand { get; set; }
    //TODO to enum
    public string Model { get; set; }

    [Display(Name = "Body type")]
    public BodyType BodyType { get; set; }

    [Display(Name = "Registration number")]
    public string RegistrationNumber { get; set; }
    public string VIN { get; set; }
    public bool Available { get; set; }

    [Display(Name = "Production year")]
    public int ProductionYear { get; set; }
    public int Mileage { get; set; }

    [Column(TypeName = "decimal(7,2)")]
    [Display(Name = "Price per day")]
    public decimal PricePerDay { get; set; }
    public byte[] Image { get; set; }

    public ICollection<Rent> Rents { get; set; }
}
