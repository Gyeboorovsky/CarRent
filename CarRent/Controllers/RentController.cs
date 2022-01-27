using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.DAL;
using CarRent.Models.Rent;
using CarRent.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{

    [Authorize(Roles = "User,Employee,Administrator")]
    public class RentController : Controller
    {
        private readonly IRentService _rentService;
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<User> _userManager;

        public RentController(IRentService rentService, IVehicleService vehicleService, UserManager<User> userManager)
        {
            _rentService = rentService;
            _vehicleService = vehicleService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AllActiveRents()
        {
            var model = new List<RentViewModel>();
            var rents = await _rentService.GetAll();

            foreach (var rent in rents)
            {
                var rentViewModel = new RentViewModel()
                {
                    RentId = rent.Id,
                    BookingDateTime = rent.BookingDateTime,
                    PlannedDateOfReceipt = rent.PlannedDateOfReceipt,
                    DateOfReceipt = rent.DateOfReceipt,
                    DateOfReturn = rent.DateOfReturn,
                    Description = rent.Description,
                    RentPeriod = rent.RentPeriod,
                    Receipt = rent.Receipt,
                    RentedVehicle = await _vehicleService.GetById(rent.VehicleId),
                    RentUser = await _userManager.FindByIdAsync(rent.UserId)
                };

                model.Add(rentViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RentVehicle(Guid rentId)
        {
            var rentToUpdate = await _rentService.GetById(rentId);
            if (rentToUpdate != null)
            {
                await _rentService.SetDateOfReceipt(rentToUpdate, DateTime.Now);
            }

            return RedirectToAction("AllActiveRents");
        }

        [HttpGet]
        public async Task<IActionResult> ReturnVehicle(Guid rentId)
        {
            var rentToUpdate = await _rentService.GetById(rentId);

            if (rentToUpdate != null)
            {
                await _rentService.SetDateOfReturn(rentToUpdate, DateTime.Now);

                var selectedVehicle = await _vehicleService.GetById(rentToUpdate.VehicleId);
                await _vehicleService.UpdateAvailability(selectedVehicle, true);
            }

            return RedirectToAction("AllActiveRents");
        }

        [HttpGet]
        public IActionResult AddRentView(Guid vehicleid)
        {
            return View(new Rent {VehicleId = vehicleid});
        }

        [HttpGet]
        public async Task<IActionResult> MyRents(string userId)
        {
            var model = new List<RentViewModel>();
            var userRents = await _rentService.GetByUserId(userId);

            foreach (var rent in userRents)
            {
                var rentViewModel = new RentViewModel()
                {
                    RentId = rent.Id,
                    BookingDateTime = rent.BookingDateTime,
                    PlannedDateOfReceipt = rent.PlannedDateOfReceipt,
                    DateOfReceipt = rent.DateOfReceipt,
                    DateOfReturn = rent.DateOfReturn,
                    Description = rent.Description,
                    RentPeriod = rent.RentPeriod,
                    Receipt = rent.Receipt,
                    RentedVehicle = await _vehicleService.GetById(rent.VehicleId),
                    RentUser = await _userManager.FindByIdAsync(rent.UserId)
                };

                model.Add(rentViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostRent(Rent rent)
        {
            var selectedVehicle = await _vehicleService.GetById(rent.VehicleId);

            //Set vehicle as rented
            await _vehicleService.UpdateAvailability(selectedVehicle, false);

            //Fill rest of Rent fields and add 
            rent.BookingDateTime = DateTime.Now;
            rent.Receipt = selectedVehicle.PricePerDay * rent.RentPeriod;
            rent.UserId = _userManager.GetUserId(this.User);
            await _rentService.Add(rent);

            return RedirectToAction("MyRents", new {userId = rent.UserId});
        }
    }
}