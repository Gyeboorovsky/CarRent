using CarRent.DAL;

namespace CarRent.Models.Rent
{
	public class RentViewModel
	{
		public Guid RentId { get; set; }
		public DateTime BookingDateTime { get; set; }
        public DateTime PlannedDateOfReceipt { get; set; }
        public DateTime? DateOfReceipt { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public string? Description { get; set; }
        public int RentPeriod { get; set; }
        public decimal Receipt { get; set; }
        public Vehicle RentedVehicle { get; set; }
        public User RentUser { get; set; }
    }
}
