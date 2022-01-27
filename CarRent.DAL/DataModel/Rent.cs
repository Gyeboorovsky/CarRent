using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRent.Common;

namespace CarRent.DAL
{

    public class Rent
    {
        [Key] public Guid Id { get; set; }
        [Display(Name = "Booking date")] public DateTime BookingDateTime { get; set; }

        [Display(Name = "Expected receipt date")]
        public DateTime PlannedDateOfReceipt { get; set; }

        [Display(Name = "Receipt date")] public DateTime? DateOfReceipt { get; set; }
        [Display(Name = "Return date")] public DateTime? DateOfReturn { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Rent period (days)")] public int RentPeriod { get; set; }
        [Column(TypeName = "decimal(10,2)")] public decimal Receipt { get; set; }
        public Guid VehicleId { get; set; }
        public string UserId { get; set; }
    }
}